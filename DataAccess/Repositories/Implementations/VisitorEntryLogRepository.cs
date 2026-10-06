using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class VisitorEntryLogRepository : IVisitorEntryLogRepository
    {
        private readonly GirlsHostelDbContext _context;

        public VisitorEntryLogRepository(GirlsHostelDbContext context)
        {
            _context = context;
        }

        // ======================
        // IRepository Methods
        // ======================

        public async Task<IEnumerable<VisitorEntryLog>> GetAllAsync()
        {
            return await _context.VisitorEntryLogs
                .Include(x => x.Visitor)
                .Include(x => x.Girl)
                .ToListAsync();
        }

        public async Task<VisitorEntryLog?> GetByIdAsync(int id)
        {
            return await _context.VisitorEntryLogs
                .Include(x => x.Visitor)
                .Include(x => x.Girl)
                .FirstOrDefaultAsync(x => x.VisitorEntryLogId == id);
        }

        public async Task AddAsync(VisitorEntryLog entity)
        {
            await _context.VisitorEntryLogs.AddAsync(entity);
        }

        public void Update(VisitorEntryLog entity)
        {
            _context.VisitorEntryLogs.Update(entity);
        }

        public void Remove(VisitorEntryLog entity)
        {
            _context.VisitorEntryLogs.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // ======================
        // Custom Methods
        // ======================

        // Today's visits
        public async Task<IEnumerable<VisitorEntryLog>> GetTodayVisitsAsync()
        {
            var today = DateTime.Today;

            return await _context.VisitorEntryLogs
                .Include(x => x.Visitor)
                .Include(x => x.Girl)
                .Where(x => x.VisitDate.Date == today)
                .ToListAsync();
        }

        // Visits for a specific girl
        public async Task<IEnumerable<VisitorEntryLog>> GetGirlVisitsAsync(int girlId)
        {
            return await _context.VisitorEntryLogs
                .Include(x => x.Visitor)
                .Where(x => x.GirlId == girlId)
                .OrderByDescending(x => x.VisitDate)
                .ToListAsync();
        }

        // Visitor history
        public async Task<IEnumerable<VisitorEntryLog>> GetVisitorHistoryAsync(int visitorId)
        {
            return await _context.VisitorEntryLogs
                .Include(x => x.Girl)
                .Where(x => x.VisitorId == visitorId)
                .OrderByDescending(x => x.VisitDate)
                .ToListAsync();
        }

        // Visitors currently inside hostel
        public async Task<IEnumerable<VisitorEntryLog>> GetVisitorsCurrentlyInsideAsync()
        {
            return await _context.VisitorEntryLogs
                .Include(x => x.Visitor)
                .Include(x => x.Girl)
                .Where(x => x.OutTime == null)
                .ToListAsync();
        }

        // Mark visitor exit
        public async Task MarkVisitorExitAsync(int visitorEntryLogId)
        {
            var log = await _context.VisitorEntryLogs
                .FirstOrDefaultAsync(x => x.VisitorEntryLogId == visitorEntryLogId);

            if (log != null)
            {
                log.OutTime = DateTime.Now;

                _context.VisitorEntryLogs.Update(log);

                await _context.SaveChangesAsync();
            }
        }
    }
}
