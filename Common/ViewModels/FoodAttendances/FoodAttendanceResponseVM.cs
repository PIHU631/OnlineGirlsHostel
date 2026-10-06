using Common.Enums;

namespace Common.ViewModels.FoodAttendances
{
    public class FoodAttendanceResponseVM
    {
        public int FoodAttendanceId { get; set; }

        public int GirlId { get; set; }

        public string? GirlName { get; set; }

        public DateTime Date { get; set; }

        public MealType MealType { get; set; }

        public string MealTypeName => MealType.ToString();
    }
}
