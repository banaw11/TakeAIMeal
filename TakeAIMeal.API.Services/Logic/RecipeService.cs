using Newtonsoft.Json;
using TakeAIMeal.API.Services.Extensions;
using TakeAIMeal.API.Services.Interfaces;
using TakeAIMeal.API.Services.Models;
using TakeAIMeal.Common.Dictionaries;
using TakeAIMeal.Common.Resources;
using TakeAIMeal.Common.Services.Interfaces;
using TakeAIMeal.Data;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.API.Services.Logic
{
    public class RecipeService : IRecipeService
    {
        private readonly IImageService _imageService;
        private readonly ITextGeneratorService _textGeneratorService;
        private readonly ITextRecognitionService _textRecognitionService;
        private readonly ITranslateService _translateService;
        private readonly IProductRepository _productRepository;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IUserDietRepository _userDietRepository;

        public RecipeService(IImageService imageService, ITextGeneratorService textGeneratorService, ITextRecognitionService textRecognitionService, ITranslateService translateService,
            IProductRepository productRepository, IBlobStorageService blobStorageService, IRecipeRepository recipeRepository, IUserIdentityService userIdentityService, IUserDietRepository userDietRepository)
        {
            _imageService = imageService;
            _textGeneratorService = textGeneratorService;
            _textRecognitionService = textRecognitionService;
            _translateService = translateService;
            _productRepository = productRepository;
            _blobStorageService = blobStorageService;
            _recipeRepository = recipeRepository;
            _userIdentityService = userIdentityService;
            _userDietRepository = userDietRepository;
        }

        public string GetRecipeIngridientsFromProducts(ICollection<int> productIds)
        {
            if(productIds != null && productIds.Count > 0)
            {
                productIds = productIds.Distinct().ToList();
                var ingredients = _productRepository.Where(x => productIds.Contains(x.Id))
                .Select(x => x.Name.ToLower())
                .ToList();

                return string.Join(", ", ingredients);
            }

            return string.Empty;
        }

        /// <inheritdoc/>
        public async Task<Tuple<RecipeModel, Guid>> GenerateRecipe(string prompt, string language, MealTypes mealType)
        {
            if(!string.IsNullOrEmpty(prompt) && !string.IsNullOrEmpty(language))
            {
                RecipeModel model = new RecipeModel();

                var recipeResponse = await _textGeneratorService.GenerateText(prompt);
                if(recipeResponse != null && recipeResponse.Count > 0)
                {
                    model.Recipe = recipeResponse.FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(model.Recipe))
                {
                    var recognitionResponse = await _textRecognitionService.GetTagsFromText(model.Recipe);
                    if(recognitionResponse != null && recognitionResponse.Count > 0)
                    {
                        List<string> tags = new List<string>();
                        foreach (var entity in recognitionResponse)
                        {
                            if (entity.Category.ToString() == "Product" && !tags.Any(x => entity.Text.ToString().Contains(x)))
                            {
                                tags.Add(entity.Text.ToString());
                            }
                        }

                        if(tags.Count > 0)
                        {
                            model.Title = await GenerateTitleFromTags(tags);
                        }

                        if (!string.IsNullOrEmpty(model.Title))
                        {
                            model.ImageBase64 = $"data:image/svg+xml;base64, {await GenerateImageFromTitle(model.Title)}";
                        }
                    }

                    var clone = model.Clone() as RecipeModel;
                    Guid identifier = Guid.NewGuid();
                    _ = Task.Run(() => UploadRecipToStorage(clone, identifier).ConfigureAwait(false));

                    if(language.ToUpper() != LanguageCodes.EN.ToString())
                    {
                        model = await TranslateRecipe(model, language);
                    }
                    var userId = _userIdentityService.UserId;
                    _recipeRepository.AddGeneratedRecipeLog(mealType, userId > 0 ? userId : null);

                    return Tuple.Create(model, identifier);
                }

            }
            return null;
        }
        
        /// <inheritdoc/>
        public async Task<RecipeModel> GetRecipe(Guid identifier, string language)
        {
            if(identifier != Guid.Empty)
            {
                var recipe = await DownloadRecipFromStorage(identifier);
                if(!string.IsNullOrEmpty(language) && language.ToUpper() != LanguageCodes.EN.ToString())
                {
                    return await TranslateRecipe(recipe, language);
                }
                return recipe;
            }
            return null;
        }

        /// <inheritdoc/>
        public async Task<Tuple<RecipeModel, int>> GetSavedRecipe(Guid identifier, string language)
        {
            var recipeId = _recipeRepository
                .Where(x => x.UserId == _userIdentityService.UserId && x.RecipeIdentifier == identifier)
                .Select(x => x.Id)
                .FirstOrDefault();

            if(recipeId > 0)
            {
                var recipe = await GetRecipe(identifier, language);
                return Tuple.Create(recipe, recipeId);
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task<int?> AddRecipe(RecipeReferenceModel model)
        {
            if(model != null && model.Identifier != Guid.Empty)
            {
                var recipe = new Recipe
                {
                    UserId = _userIdentityService.UserId,
                    RecipeIdentifier = model.Identifier,
                    MealType = (int)model.MealType
                };

                var recipeModel = await DownloadRecipFromStorage(model.Identifier);

                if(!_recipeRepository.Any(x => x.UserId == recipe.UserId && x.RecipeIdentifier == model.Identifier) && recipeModel != null)
                {
                    _recipeRepository.Add(recipe);
                    _recipeRepository.SaveChanges();

                    _ = Task.Run(() => SaveRecipeIntoUserBlob(_userIdentityService.EmailAddress, recipeModel, model.Identifier));

                    return recipe.Id;
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public void RemoveRecipe(int recipeId)
        {
            var recipe = _recipeRepository.Get(x => x.Id == recipeId);
            if(recipe != null)
            {
                _ = Task.Run(() => RemoveRecipeFromUserBlob(_userIdentityService.EmailAddress, recipe.RecipeIdentifier));

                _recipeRepository.Delete(recipe);
                _recipeRepository.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public async Task<ICollection<RecipeCollectionModel>> GetRecipeCollection(string language)
        {
            ICollection<RecipeCollectionModel> recipes = new List<RecipeCollectionModel>();

            var userEmail = _userIdentityService.EmailAddress;
            if (!string.IsNullOrEmpty(userEmail))
            {
                var identifier = EmailIdentifierGenerator.GenerateIdentifier(userEmail);

                recipes = await DownloadRecipCollectionFromStorage(identifier);

                if (!string.IsNullOrEmpty(language) && language.ToUpper() != LanguageCodes.EN.ToString())
                {
                    recipes = await TranslateRecipeCollection(recipes, language);
                }
            }

            return recipes;
        }

        /// <inheritdoc/>
        public ICollection<int> GetUserProductExclussionsByDiet(DietTypes dietType)
        {
            var userId = _userIdentityService.UserId;
            return _userDietRepository.Where(x => x.UserId == userId && x.DietType == (int)dietType)
                .Select(x => x.UserProductsExclusions.Select(x => x.ProductId).ToList())
                .FirstOrDefault();
        }

        #region private methods
        /// <summary>
        /// Generates a dish title from a list of tags.
        /// </summary>
        /// <param name="tags">A list of tags to use as input for the title generation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated title as a string.</returns> 
        private async Task<string> GenerateTitleFromTags(List<string> tags)
        {
            if(tags != null && tags.Count > 0)
            {
                var prompt = string.Format(Prompts.DishTitleFromTags, string.Join(", ",tags));
                var titleResponse = await _textGeneratorService.GenerateText(prompt, maxTokens: 50);
                if(titleResponse != null && titleResponse.Count > 0)
                {
                    return titleResponse.FirstOrDefault();
                }
            }
            return null;
        }

        /// <summary>
        /// Generates an image in base64 format from a given title.
        /// </summary>
        /// <param name="title">The title used to generate the image.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated image as a string in base64 format.</returns>
        private async Task<string> GenerateImageFromTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                return await _imageService.GenerateImageToBase64(title);
            }
            return null;
        }

        /// <summary>
        /// Translates a recipe into the specified language.
        /// </summary>
        /// <param name="recipe">The recipe model to be translated.</param>
        /// <param name="language">The language to translate the recipe into.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a recipe model with the translated text.</returns>
        private async Task<RecipeModel> TranslateRecipe(RecipeModel recipe, string language)
        {
            if(recipe != null && !string.IsNullOrEmpty(language))
            {
                RecipeModel model = new RecipeModel { ImageBase64 = recipe.ImageBase64 };

                if (!string.IsNullOrEmpty(recipe.Recipe))
                {
                    model.Recipe = await _translateService.TranslateText(recipe.Recipe, language);
                }

                if (!string.IsNullOrEmpty(recipe.Title))
                {
                    model.Title = await _translateService.TranslateText(recipe.Title, language);
                }

                return model;
            }

            return recipe;
        }

        /// <summary>
        /// Translates the titles of recipe collections using the specified language.
        /// </summary>
        /// <param name="recipes">The collection of recipe collections to translate.</param>
        /// <param name="language">The target language for translation.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// The task result contains the translated collection of <see cref="RecipeCollectionModel"/> objects.
        /// </returns>
        private async Task<ICollection<RecipeCollectionModel>> TranslateRecipeCollection(ICollection<RecipeCollectionModel> recipes, string language)
        {
            var titles = recipes.Select(x => x.Title).ToList();
            var translations = await _translateService.TranslateTexts(titles, language);
            if (translations != null && translations.Count == recipes.Count)
            {
                int counter = 0;
                foreach (var translation in translations)
                {
                    recipes.ElementAt(counter).Title = translation;
                    counter++;
                }
            }

            return recipes;
        }

        /// <summary>
        /// Uploads a recipe to Azure Blob Storage.
        /// </summary>
        /// <param name="recipe">The recipe to be uploaded.</param>
        /// <param name="recipeIdentifier">The identifier of the recipe.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task UploadRecipToStorage(RecipeModel recipe, Guid recipeIdentifier)
        {
            try
            {
                if (recipe != null)
                {
                    var serializedObject = JsonConvert.SerializeObject(recipe);
                    await _blobStorageService.UploadStringContent(serializedObject, BlobStorageContainerNames.RecipeContainer, recipeIdentifier.ToString());
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Downloads a recipe from Azure Blob Storage.
        /// </summary>
        /// <param name="recipeIdentifier">The identifier of the recipe to be downloaded.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the downloaded recipe.</returns>
        private async Task<RecipeModel> DownloadRecipFromStorage(Guid recipeIdentifier)
        {
            RecipeModel recipe = null;
            var serializedObject = await _blobStorageService.DownloadStringContent(BlobStorageContainerNames.RecipeContainer, recipeIdentifier.ToString());
            if(!string.IsNullOrEmpty(serializedObject))
            {
                recipe = JsonConvert.DeserializeObject<RecipeModel>(serializedObject);
            }
            return recipe;
        }

        /// <summary>
        /// Downloads the recipe collection from the storage for the specified <paramref name="emailIdentifier"/>.
        /// </summary>
        /// <param name="emailIdentifier">The email identifier associated with the recipe collection.</param>
        /// <returns>A list of recipe collection models.</returns>
        private async Task<List<RecipeCollectionModel>> DownloadRecipCollectionFromStorage(string emailIdentifier)
        {
            List<RecipeCollectionModel> recipeCollection = new List<RecipeCollectionModel>();
            try
            {
                if (!string.IsNullOrEmpty(emailIdentifier))
                {
                    var serializedObject = await _blobStorageService.DownloadStringContent(BlobStorageContainerNames.UserCollectionContainer, emailIdentifier.ToLower());
                    if (!string.IsNullOrEmpty(serializedObject))
                    {
                        recipeCollection = JsonConvert.DeserializeObject<List<RecipeCollectionModel>>(serializedObject);
                    }
                }
            }
            catch (Exception)
            {
                // Handle exception if needed
            }

            return recipeCollection;
        }

        /// <summary>
        /// Uploads the recipe collection to the storage for the specified <paramref name="emailIdentifier"/>.
        /// </summary>
        /// <param name="emailIdentifier">The email identifier associated with the recipe collection.</param>
        /// <param name="collection">The list of recipe collection models to upload.</param>
        private async Task UploadRecipCollectionToStorage(string emailIdentifier, List<RecipeCollectionModel> collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(emailIdentifier) && collection != null)
                {
                    var serializedObject = JsonConvert.SerializeObject(collection);

                    await _blobStorageService.UploadStringContent(serializedObject, BlobStorageContainerNames.UserCollectionContainer, emailIdentifier.ToLower());
                }
            }
            catch (Exception)
            {
                // Handle exception if needed
            }
        }

        /// <summary>
        /// Saves a recipe into the user's blob storage associated with the specified <paramref name="email"/>.
        /// </summary>
        /// <param name="email">The email associated with the user.</param>
        /// <param name="recipe">The recipe to save.</param>
        /// <param name="recipeIdentifier">The unique identifier for the recipe.</param>
        private async Task SaveRecipeIntoUserBlob(string email, RecipeModel recipe, Guid recipeIdentifier)
        {
            var emailIdentifier = EmailIdentifierGenerator.GenerateIdentifier(email);
            List<RecipeCollectionModel> recipeCollection = await DownloadRecipCollectionFromStorage(emailIdentifier);

            recipeCollection.Add(new RecipeCollectionModel
            {
                ImageBase64 = recipe.ImageBase64,
                Title = recipe.Title,
                RecipeIdentifier = recipeIdentifier
            });

            await UploadRecipCollectionToStorage(emailIdentifier, recipeCollection);
        }

        /// <summary>
        /// Removes a recipe from the user's blob storage associated with the specified <paramref name="email"/>.
        /// </summary>
        /// <param name="email">The email associated with the user.</param>
        /// <param name="recipeIdentifier">The unique identifier for the recipe to remove.</param>
        private async Task RemoveRecipeFromUserBlob(string email, Guid recipeIdentifier)
        {
            if (recipeIdentifier != Guid.Empty)
            {
                var emailIdentifier = EmailIdentifierGenerator.GenerateIdentifier(email);
                List<RecipeCollectionModel> recipeCollection = await DownloadRecipCollectionFromStorage(emailIdentifier);

                var toRemove = recipeCollection.Where(x => x.RecipeIdentifier == recipeIdentifier).FirstOrDefault();
                if (toRemove != null)
                {
                    recipeCollection.Remove(toRemove);
                }

                await UploadRecipCollectionToStorage(emailIdentifier, recipeCollection);
            }
        }
        #endregion
    }
}
