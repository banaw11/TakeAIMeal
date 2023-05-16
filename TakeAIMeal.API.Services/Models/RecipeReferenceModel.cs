using TakeAIMeal.Common.Dictionaries;

namespace TakeAIMeal.API.Services.Models
{
    public class RecipeReferenceModel
    {
        public Guid Identifier { get; set; }
        public MealTypes MealType { get; set; }
    }
}
