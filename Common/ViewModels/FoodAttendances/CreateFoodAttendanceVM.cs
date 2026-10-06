using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.FoodAttendances
{
    public class CreateFoodAttendanceVM
    {
        [Required(ErrorMessage = "GirlId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid GirlId")]
        public int GirlId { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "MealType is required")]
        [EnumDataType(typeof(MealType))]
        public MealType MealType { get; set; }
    }
}
