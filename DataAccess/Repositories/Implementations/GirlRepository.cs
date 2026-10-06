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
    public class GirlRepository : IGirlRepository
    {
        private readonly GirlsHostelDbContext _context;

        public GirlRepository(GirlsHostelDbContext context)
        {
            _context = context;
        }

        // =====================
        // IRepository Methods
        // =====================

        public async Task<IEnumerable<Girl>> GetAllAsync()
        {
            return await _context.Girls
                .Include(x => x.Room)
                .ToListAsync();
        }

        public async Task<Girl?> GetByIdAsync(int id)
        {
            return await _context.Girls
                .Include(x => x.Room)
                .FirstOrDefaultAsync(x => x.GirlId == id);
        }

        public async Task AddAsync(Girl entity)
        {
            await _context.Girls.AddAsync(entity);
        }

        public void Update(Girl entity)
        {
            _context.Girls.Update(entity);
        }

        public void Remove(Girl entity)
        {
            _context.Girls.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // =====================
        // Custom Methods
        // =====================

        // Girls in specific room
        public async Task<IEnumerable<Girl>> GetGirlsByRoomAsync(int roomId)
        {
            return await _context.Girls
                .Where(x => x.RoomId == roomId)
                .ToListAsync();
        }

        // Girls in specific hostel
        public async Task<IEnumerable<Girl>> GetGirlsByHostelAsync(int hostelId)
        {
            return await _context.Girls
                .Include(x => x.Room)
                .Where(x => x.Room.HostelId == hostelId)
                .ToListAsync();
        }

        // Active girls (still living in hostel)
        public async Task<IEnumerable<Girl>> GetActiveGirlsAsync()
        {
            return await _context.Girls
                .Where(x => x.IsActive)
                .Include(x => x.Room)
                .ToListAsync();
        }

        // Girl full details
        public async Task<Girl?> GetGirlWithDetailsAsync(int girlId)
        {
            return await _context.Girls
                .Include(x => x.Room)
                .Include(x => x.RoomHistories)
                .Include(x => x.EntryLogs)
                .Include(x => x.MarketVisits)
                .Include(x => x.FoodAttendances)
                .Include(x => x.HostelAttendances)
                .FirstOrDefaultAsync(x => x.GirlId == girlId);
        }
    }
}
