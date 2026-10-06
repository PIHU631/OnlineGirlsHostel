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
    public class RoomHistoryRepository : IRoomHistoryRepository
    {
        private readonly GirlsHostelDbContext _context;

        public RoomHistoryRepository(GirlsHostelDbContext context)
        {
            _context = context;
        }

        // =========================
        // IRepository Methods
        // =========================

        public async Task<IEnumerable<RoomHistory>> GetAllAsync()
        {
            return await _context.RoomHistories
                .Include(x => x.Girl)
                .Include(x => x.Room)
                .ToListAsync();
        }

        public async Task<RoomHistory?> GetByIdAsync(int id)
        {
            return await _context.RoomHistories
                .Include(x => x.Girl)
                .Include(x => x.Room)
                .FirstOrDefaultAsync(x => x.RoomHistoryId == id);
        }

        public async Task AddAsync(RoomHistory entity)
        {
            await _context.RoomHistories.AddAsync(entity);
        }

        public void Update(RoomHistory entity)
        {
            _context.RoomHistories.Update(entity);
        }

        public void Remove(RoomHistory entity)
        {
            _context.RoomHistories.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // =========================
        // Custom Methods
        // =========================

        // Girl room history
        public async Task<IEnumerable<RoomHistory>> GetGirlRoomHistoryAsync(int girlId)
        {
            return await _context.RoomHistories
                .Include(x => x.Room)
                .Where(x => x.GirlId == girlId)
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();
        }

        // Current room of girl
        public async Task<RoomHistory?> GetCurrentRoomAsync(int girlId)
        {
            return await _context.RoomHistories
                .Include(x => x.Room)
                .FirstOrDefaultAsync(x => x.GirlId == girlId && x.EndDate == null);
        }

        // Room history by room
        public async Task<IEnumerable<RoomHistory>> GetRoomHistoryByRoomAsync(int roomId)
        {
            return await _context.RoomHistories
                .Include(x => x.Girl)
                .Where(x => x.RoomId == roomId)
                .ToListAsync();
        }

        // End current room (when girl shifts room)
        public async Task EndCurrentRoomAsync(int girlId)
        {
            var currentRoom = await _context.RoomHistories
                .FirstOrDefaultAsync(x => x.GirlId == girlId && x.EndDate == null);

            if (currentRoom != null)
            {
                currentRoom.EndDate = DateTime.Now;

                _context.RoomHistories.Update(currentRoom);

                await _context.SaveChangesAsync();
            }
        }
    }
}
