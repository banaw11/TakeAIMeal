using System.ComponentModel.DataAnnotations;
using TakeAIMeal.Common.Resources;

namespace TakeAIMeal.Common.Dictionaries
{
    public enum MealTypes
    {
        [Display(Name = nameof(Breakfast), ResourceType = typeof(Recipes))]
        Breakfast = 1,

        [Display(Name = nameof(Lunch), ResourceType = typeof(Recipes))]
        Lunch = 2,

        [Display(Name = nameof(Dinner), ResourceType = typeof(Recipes))]
        Dinner = 3
    }
}
