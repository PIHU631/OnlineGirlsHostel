using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementations
{
    public class EntryLogRepository : IEntryLogRepository
    {
        private readonly GirlsHostelDbContext _context;

        public EntryLogRepository(GirlsHostelDbContext context)
        {
            _context = context;
        }

        // =========================
        // IRepository Methods
        // =========================

        public async Task<IEnumerable<EntryLog>> GetAllAsync()
        {
            return await _context.EntryLogs
                .Include(x => x.Girl)
                .ToListAsync();
        }

        public async Task<EntryLog?> GetByIdAsync(int id)
        {
            return await _context.EntryLogs
                .Include(x => x.Girl)
                .FirstOrDefaultAsync(x => x.EntryLogId == id);
        }

        public async Task AddAsync(EntryLog entity)
        {
            await _context.EntryLogs.AddAsync(entity);
        }

        public void Update(EntryLog entity)
        {
            _context.EntryLogs.Update(entity);
        }

        public void Remove(EntryLog entity)
        {
            _context.EntryLogs.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


        // =========================
        // IEntryLogRepository Methods
        // =========================


        // Girls currently outside hostel
        public async Task<IEnumerable<EntryLog>> GetGirlsCurrentlyOutsideAsync()
        {
            return await _context.EntryLogs
                .Include(x => x.Girl)
                .Where(x => x.InTime == null)
                .ToListAsync();
        }


        // Today outing logs
        public async Task<IEnumerable<EntryLog>> GetTodayEntryLogsAsync()
        {
            var today = DateTime.Today;

            return await _context.EntryLogs
                .Include(x => x.Girl)
                .Where(x => x.OutTime.Date == today)
                .ToListAsync();
        }


        // Monthly report
        public async Task<IEnumerable<EntryLog>> GetMonthlyEntryLogsAsync(int month, int year)
        {
            return await _context.EntryLogs
                .Include(x => x.Girl)
                .Where(x => x.OutTime.Month == month && x.OutTime.Year == year)
                .ToListAsync();
        }


        // Mark girl return
        public async Task MarkGirlReturnAsync(int entryLogId)
        {
            var log = await _context.EntryLogs
                .FirstOrDefaultAsync(x => x.EntryLogId == entryLogId);

            if (log != null)
            {
                log.InTime = DateTime.Now;

                _context.EntryLogs.Update(log);

                await _context.SaveChangesAsync();
            }
        }
    }
}
