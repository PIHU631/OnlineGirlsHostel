using Common.Enums;

namespace DataAccess.Entities
{
    public class FoodAttendance
    {
        public int FoodAttendanceId { get; set; }

        public int GirlId { get; set; }

        public DateTime Date { get; set; }

        public MealType MealType { get; set; }

        public Girl Girl { get; set; }
    }
}
