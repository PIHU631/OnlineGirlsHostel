using Common.Enums;
using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IFoodAttendanceRepository : IRepository<FoodAttendance>
    {
        Task<IEnumerable<FoodAttendance>> GetTodayFoodAttendanceAsync();

        Task<IEnumerable<FoodAttendance>> GetMonthlyFoodAttendanceAsync(int month, int year);

        Task<IEnumerable<FoodAttendance>> GetGirlFoodAttendanceAsync(int girlId);

        Task<bool> HasGirlTakenMealAsync(int girlId, DateTime date, MealType mealType);
    }
}
