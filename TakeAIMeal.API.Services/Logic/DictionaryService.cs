using System.ComponentModel.DataAnnotations;
using TakeAIMeal.API.Services.Interfaces;
using TakeAIMeal.API.Services.Models;
using TakeAIMeal.Common.Dictionaries;
using TakeAIMeal.Common.Extensions;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.API.Services.Logic
{
    public class DictionaryService : IDictionarySerivce
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Gets a collection of <see cref="DictionaryItem"/> representing the values of an <see cref="Enum"/> type.
        /// </summary>
        /// <typeparam name="T">The <see cref="Enum"/> type to get the collection for.</typeparam>
        /// <returns>A collection of <see cref="DictionaryItem"/> representing the values of the <see cref="Enum"/> type.</returns>
        private static ICollection<DictionaryItem> GetEnumCollection<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                .Select(x => new DictionaryItem { Name = x.GetAttribute<DisplayAttribute>().Name, Value = x })
                .OrderBy(x => x.Name)
                .ToList();
        }

        public DictionaryService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        /// <inheritdoc/>
        public ICollection<DictionaryItem> GetCategories()
        {
            return _categoryRepository.GetAll()
                .Select(x => new DictionaryItem { Name = x.Name, Value = x.Id })
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
                .Select(x => new DictionaryItem { Name = x.Name, Value = x.Id })
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}
