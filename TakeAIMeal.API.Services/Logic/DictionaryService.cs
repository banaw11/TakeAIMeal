using TakeAIMeal.API.Services.Interfaces;
using TakeAIMeal.API.Services.Models;
using TakeAIMeal.Common.Dictionaries;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.API.Services.Logic
{
    public class DictionaryService : IDictionarySerivce
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserDietRepository _userDietRepository;
        private readonly IUserIdentityService _userIdentityService;

        /// <summary>
        /// Gets a collection of <see cref="DictionaryItem"/> representing the values of an <see cref="Enum"/> type.
        /// </summary>
        /// <typeparam name="T">The <see cref="Enum"/> type to get the collection for.</typeparam>
        /// <returns>A collection of <see cref="DictionaryItem"/> representing the values of the <see cref="Enum"/> type.</returns>
        private static ICollection<DictionaryItem> GetEnumCollection<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                .Select(x => new DictionaryItem { Name = x.ToString(), Value = x })
                .OrderBy(x => x.Name)
                .ToList();
        }

        public DictionaryService(IProductRepository productRepository, ICategoryRepository categoryRepository, IUserDietRepository userDietRepository, IUserIdentityService userIdentityService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _userDietRepository = userDietRepository;
            _userIdentityService = userIdentityService;
        }

        /// <inheritdoc/>
        public ICollection<DictionaryItem> GetCategories()
        {
            return _categoryRepository.GetAll()
                .Select(x => new DictionaryItem { Name = x.NormalizedName, Value = x.Id })
                .OrderBy(x => x.Name)
                .ToList();
        }

        /// <inheritdoc/>
        public ICollection<DictionaryItem> GetMeals()
        {
            return GetEnumCollection<MealTypes>();
        }

        /// <inheritdoc/>
        public ICollection<DictionaryItem> GetProducts(int categoryId)
        {
            return _productRepository.Where(x => x.CategoryId == categoryId)
                .Select(x => new DictionaryItem { Name = x.NormalizedName, Value = x.Id })
                .OrderBy(x => x.Name)
                .ToList();
        }

        /// <inheritdoc/>
        public ICollection<DictionaryItem> GetAllDiets()
        {
            return GetEnumCollection<DietTypes>();
        }


        /// <inheritdoc/>
        public ICollection<DictionaryItem> GetUsedDiets()
        {
            var userId = _userIdentityService.UserId;
            if(userId > 0)
            {
                return _userDietRepository.Where(x => x.UserId == userId)
                    .Select(x => x.DietType)
                    .ToList()
                    .Select(x => new DictionaryItem { Name = ((DietTypes)x).ToString(), Value = x })
                    .OrderBy(x => x.Name)
                    .ToList();
            }
            return new List<DictionaryItem>();
        }
    }
}
