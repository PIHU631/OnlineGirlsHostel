using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class HostelAttendanceRepository : IHostelAttendanceRepository
    {
        private readonly GirlsHostelDbContext _context;

        public HostelAttendanceRepository(GirlsHostelDbContext context)
        {
            _context = context;
        }

        // =========================
        // IRepository Methods
        // =========================

        public async Task<IEnumerable<HostelAttendance>> GetAllAsync()
        {
            return await _context.HostelAttendances
                .Include(x => x.Girl)
                .ToListAsync();
        }

        public async Task<HostelAttendance?> GetByIdAsync(int id)
        {
            return await _context.HostelAttendances
                .Include(x => x.Girl)
                .FirstOrDefaultAsync(x => x.HostelAttendanceId == id);
        }

        public async Task AddAsync(HostelAttendance entity)
        {
            await _context.HostelAttendances.AddAsync(entity);
        }

        public void Update(HostelAttendance entity)
        {
            _context.HostelAttendances.Update(entity);
        }

        public void Remove(HostelAttendance entity)
        {
            _context.HostelAttendances.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // =========================
        // Custom Methods
        // =========================

        // Today attendance
        public async Task<IEnumerable<HostelAttendance>> GetTodayAttendanceAsync()
        {
            var today = DateTime.Today;

            return await _context.HostelAttendances
                .Include(x => x.Girl)
                .Where(x => x.Date.Date == today)
                .ToListAsync();
        }

        // Monthly attendance
        public async Task<IEnumerable<HostelAttendance>> GetMonthlyAttendanceAsync(int month, int year)
        {
            return await _context.HostelAttendances
                .Include(x => x.Girl)
                .Where(x => x.Date.Month == month && x.Date.Year == year)
                .ToListAsync();
        }

        // Attendance of specific girl
        public async Task<IEnumerable<HostelAttendance>> GetGirlAttendanceAsync(int girlId)
        {
            return await _context.HostelAttendances
                .Include(x => x.Girl)
                .Where(x => x.GirlId == girlId)
                .ToListAsync();
        }

        // Absent girls
        public async Task<IEnumerable<HostelAttendance>> GetAbsentGirlsAsync(DateTime date)
        {
            return await _context.HostelAttendances
                .Include(x => x.Girl)
                .Where(x => x.Date.Date == date.Date && x.IsPresent == false)
                .ToListAsync();
        }

        // Check if attendance already marked
        public async Task<bool> HasGirlMarkedAttendanceAsync(int girlId, DateTime date)
        {
            return await _context.HostelAttendances
                .AnyAsync(x =>
                    x.GirlId == girlId &&
                    x.Date.Date == date.Date);
        }
    }
}
