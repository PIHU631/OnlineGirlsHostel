using Common.Enums;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class FoodAttendanceRepository : IFoodAttendanceRepository
    {
        private readonly GirlsHostelDbContext _context;

        public FoodAttendanceRepository(GirlsHostelDbContext context)
        {
            _context = context;
        }

        // ======================
        // IRepository Methods
        // ======================

        public async Task<IEnumerable<FoodAttendance>> GetAllAsync()
        {
            return await _context.FoodAttendances
                .Include(x => x.Girl)
                .ToListAsync();
        }

        public async Task<FoodAttendance?> GetByIdAsync(int id)
        {
            return await _context.FoodAttendances
                .Include(x => x.Girl)
                .FirstOrDefaultAsync(x => x.FoodAttendanceId == id);
        }

        public async Task AddAsync(FoodAttendance entity)
        {
            await _context.FoodAttendances.AddAsync(entity);
        }

        public void Update(FoodAttendance entity)
        {
            _context.FoodAttendances.Update(entity);
        }

        public void Remove(FoodAttendance entity)
        {
            _context.FoodAttendances.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // ======================
        // Custom Methods
        // ======================

        // Today food attendance
        public async Task<IEnumerable<FoodAttendance>> GetTodayFoodAttendanceAsync()
        {
            var today = DateTime.Today;

            return await _context.FoodAttendances
                .Include(x => x.Girl)
                .Where(x => x.Date.Date == today)
                .ToListAsync();
        }

        // Monthly report
        public async Task<IEnumerable<FoodAttendance>> GetMonthlyFoodAttendanceAsync(int month, int year)
        {
            return await _context.FoodAttendances
                .Include(x => x.Girl)
                .Where(x => x.Date.Month == month && x.Date.Year == year)
                .ToListAsync();
        }

        // Girl specific report
        public async Task<IEnumerable<FoodAttendance>> GetGirlFoodAttendanceAsync(int girlId)
        {
            return await _context.FoodAttendances
                .Include(x => x.Girl)
                .Where(x => x.GirlId == girlId)
                .ToListAsync();
        }

        // Check if meal already taken
        public async Task<bool> HasGirlTakenMealAsync(int girlId, DateTime date, MealType mealType)
        {
            return await _context.FoodAttendances
                .AnyAsync(x =>
                    x.GirlId == girlId &&
                    x.Date.Date == date.Date &&
                    x.MealType == mealType);
        }
    }
}
