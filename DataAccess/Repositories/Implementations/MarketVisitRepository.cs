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
    public class MarketVisitRepository : IMarketVisitRepository
    {
        private readonly GirlsHostelDbContext _context;

        public MarketVisitRepository(GirlsHostelDbContext context)
        {
            _context = context;
        }

        // =========================
        // IRepository Methods
        // =========================

        public async Task<IEnumerable<MarketVisit>> GetAllAsync()
        {
            return await _context.MarketVisits
                .Include(x => x.Girl)
                .ToListAsync();
        }

        public async Task<MarketVisit?> GetByIdAsync(int id)
        {
            return await _context.MarketVisits
                .Include(x => x.Girl)
                .FirstOrDefaultAsync(x => x.MarketVisitId == id);
        }

        public async Task AddAsync(MarketVisit entity)
        {
            await _context.MarketVisits.AddAsync(entity);
        }

        public void Update(MarketVisit entity)
        {
            _context.MarketVisits.Update(entity);
        }

        public void Remove(MarketVisit entity)
        {
            _context.MarketVisits.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // =========================
        // Custom Methods
        // =========================

        // Today market visits
        public async Task<IEnumerable<MarketVisit>> GetTodayMarketVisitsAsync()
        {
            var today = DateTime.Today;

            return await _context.MarketVisits
                .Include(x => x.Girl)
                .Where(x => x.OutTime.Date == today)
                .ToListAsync();
        }

        // Monthly report
        public async Task<IEnumerable<MarketVisit>> GetMonthlyMarketVisitsAsync(int month, int year)
        {
            return await _context.MarketVisits
                .Include(x => x.Girl)
                .Where(x => x.OutTime.Month == month && x.OutTime.Year == year)
                .ToListAsync();
        }

        // Girl specific visits
        public async Task<IEnumerable<MarketVisit>> GetGirlMarketVisitsAsync(int girlId)
        {
            return await _context.MarketVisits
                .Include(x => x.Girl)
                .Where(x => x.GirlId == girlId)
                .ToListAsync();
        }

        // Girls currently in market
        public async Task<IEnumerable<MarketVisit>> GetGirlsCurrentlyInMarketAsync()
        {
            return await _context.MarketVisits
                .Include(x => x.Girl)
                .Where(x => x.InTime == null)
                .ToListAsync();
        }

        // Mark return
        public async Task MarkReturnAsync(int marketVisitId)
        {
            var visit = await _context.MarketVisits
                .FirstOrDefaultAsync(x => x.MarketVisitId == marketVisitId);

            if (visit != null)
            {
                visit.InTime = DateTime.Now;

                _context.MarketVisits.Update(visit);

                await _context.SaveChangesAsync();
            }
        }
    }
}
