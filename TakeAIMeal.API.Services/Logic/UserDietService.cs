using Microsoft.EntityFrameworkCore;
using TakeAIMeal.API.Services.Interfaces;
using TakeAIMeal.API.Services.Models;
using TakeAIMeal.Data;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.API.Services.Logic
{
    public class UserDietService : IUserDietService
    {
        private readonly IUserDietRepository _userDietRepository;
        private readonly IUserProductExclusionRepository _userProductExclusionRepository;
        private readonly IUserIdentityService _userIdentityService;

        public UserDietService(IUserDietRepository userDietRepository, IUserProductExclusionRepository userProductExclusionRepository, IUserIdentityService userIdentityService)
        {
            _userDietRepository = userDietRepository;
            _userProductExclusionRepository = userProductExclusionRepository;
            _userIdentityService = userIdentityService;
        }

        /// <inheritdoc/>
        public void PatchDiet(UserDietModel model)
        {
            if(model != null)
            {
                var userId = _userIdentityService.UserId;
                if (_userDietRepository.Any(x => x.UserId == userId && x.DietType == (int)model.DietType))
                {
                    UpdateDiet(userId, model);
                }
                else
                {
                    AddDiet(userId, model);
                }

                _userDietRepository.SaveChanges();
            }
        }

        #region private methods

        /// <summary>
        /// Adds a user's diet to the database.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="model">The <see cref="UserDietModel"/> containing the diet information.</param>
        private void AddDiet(int userId, UserDietModel model)
        {
            var diet = new UserDiet
            {
                DietType = (int)model.DietType,
                UserId = userId,
                UserProductsExclusions = new List<UserProductsExclusion>()
            };

            var products = model.ProductExclusions.Select(x => x.ProductIds)
                .SelectMany(x => x)
                .Distinct()
                .ToList();

            foreach (var product in products)
            {
                diet.UserProductsExclusions.Add(new UserProductsExclusion
                {
                    ProductId = product,
                    UserDiet = diet
                });
            }

            _userDietRepository.Add(diet);
        }

        /// <summary>
        /// Updates a user's diet in the database.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="model">The <see cref="UserDietModel"/> containing the updated diet information.</param>
        private void UpdateDiet(int userId, UserDietModel model)
        {
            var diet = _userDietRepository
                .Where(x => x.UserId == userId && x.DietType == (int)model.DietType)
                .Include(x => x.UserProductsExclusions)
                .FirstOrDefault();

            if(diet != null)
            {
                var products = model.ProductExclusions.Select(x => x.ProductIds)
                    .SelectMany(x => x)
                    .Distinct()
                    .ToList();
                var currentProducts = diet.UserProductsExclusions.Select(x => x.ProductId).ToList();

                var toRemove = diet.UserProductsExclusions.Where(x => !products.Contains(x.ProductId));
                var toAdd = products.Where(x => !currentProducts.Contains(x));

                foreach (var product in toRemove)
                {
                    _userProductExclusionRepository.Delete(product);
                }

                foreach (var product in toAdd)
                {
                    _userProductExclusionRepository.Add(new UserProductsExclusion
                    {
                        ProductId = product,
                        UserDietId = diet.Id
                    });
                }
            }
        }

        #endregion
    }
}
