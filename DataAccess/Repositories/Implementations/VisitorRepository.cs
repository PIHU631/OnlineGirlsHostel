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
    public class VisitorRepository : IVisitorRepository
    {
        private readonly GirlsHostelDbContext _context;

        public VisitorRepository(GirlsHostelDbContext context)
        {
            _context = context;
        }

        // ======================
        // IRepository Methods
        // ======================

        public async Task<IEnumerable<Visitor>> GetAllAsync()
        {
            return await _context.Visitors.ToListAsync();
        }

        public async Task<Visitor?> GetByIdAsync(int id)
        {
            return await _context.Visitors
                .FirstOrDefaultAsync(x => x.VisitorId == id);
        }

        public async Task AddAsync(Visitor entity)
        {
            await _context.Visitors.AddAsync(entity);
        }

        public void Update(Visitor entity)
        {
            _context.Visitors.Update(entity);
        }

        public void Remove(Visitor entity)
        {
            _context.Visitors.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // ======================
        // Custom Methods
        // ======================

        // Visitor with entry logs
        public async Task<Visitor?> GetVisitorWithLogsAsync(int visitorId)
        {
            return await _context.Visitors
                .Include(x => x.VisitorEntryLogs)
                .FirstOrDefaultAsync(x => x.VisitorId == visitorId);
        }

        // Search visitor
        public async Task<IEnumerable<Visitor>> SearchVisitorsAsync(string keyword)
        {
            return await _context.Visitors
                .Where(x => x.Name.Contains(keyword) ||
                            x.Phone.Contains(keyword) ||
                            x.Relation.Contains(keyword))
                .ToListAsync();
        }

        // Find visitor by phone
        public async Task<Visitor?> GetVisitorByPhoneAsync(string phone)
        {
            return await _context.Visitors
                .FirstOrDefaultAsync(x => x.Phone == phone);
        }
    }
}
