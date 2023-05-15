using System.ComponentModel.DataAnnotations;
using TakeAIMeal.Common.Resources;

namespace TakeAIMeal.Common.Dictionaries
{
    public enum MealTypes
    {
        [Display(Name = nameof(Breakfast), ResourceType = typeof(Recipe))]
        Breakfast = 1,

        [Display(Name = nameof(Lunch), ResourceType = typeof(Recipe))]
        Lunch = 2,

        [Display(Name = nameof(Dinner), ResourceType = typeof(Recipe))]
        Dinner = 3
    }
}
