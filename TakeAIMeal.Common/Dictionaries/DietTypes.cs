using System.ComponentModel.DataAnnotations;
using TakeAIMeal.Common.Resources;

namespace TakeAIMeal.Common.Dictionaries
{
    public enum DietTypes
    {
        [Display(Name = nameof(Basic), ResourceType = typeof(Recipes))]
        Basic = 1,

        [Display(Name = nameof(Soft), ResourceType = typeof(Recipes))]
        Soft = 2,

        [Display(Name = nameof(Protein), ResourceType = typeof(Recipes))]
        Protein = 3,

        [Display(Name = nameof(LowCarb), ResourceType = typeof(Recipes))]
        LowCarb = 4,

        [Display(Name = nameof(Vege), ResourceType = typeof(Recipes))]
        Vege = 5
    }
}
