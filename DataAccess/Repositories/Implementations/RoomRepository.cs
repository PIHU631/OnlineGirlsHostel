using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class RoomRepository : IRoomRepository
    {
        private readonly GirlsHostelDbContext _context;

        public RoomRepository(GirlsHostelDbContext context)
        {
            _context = context;
        }

        // =====================
        // IRepository Methods
        // =====================

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms
                .Include(x => x.Hostel)
                .ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _context.Rooms
                .Include(x => x.Hostel)
                .FirstOrDefaultAsync(x => x.RoomId == id);
        }

        public async Task AddAsync(Room entity)
        {
            await _context.Rooms.AddAsync(entity);
        }

        public void Update(Room entity)
        {
            _context.Rooms.Update(entity);
        }

        public void Remove(Room entity)
        {
            _context.Rooms.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // =====================
        // Custom Methods
        // =====================

        // Rooms of specific hostel
        public async Task<IEnumerable<Room>> GetRoomsByHostelAsync(int hostelId)
        {
            return await _context.Rooms
                .Where(x => x.HostelId == hostelId)
                .ToListAsync();
        }

        // Room with girls
        public async Task<Room?> GetRoomWithGirlsAsync(int roomId)
        {
            return await _context.Rooms
                .Include(x => x.Girls)
                .FirstOrDefaultAsync(x => x.RoomId == roomId);
        }

        // Available rooms
        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(int hostelId)
        {
            return await _context.Rooms
                .Include(x => x.Girls)
                .Where(x => x.HostelId == hostelId &&
                            x.IsActive &&
                            x.Girls.Count < x.Capacity)
                .ToListAsync();
        }

        // Room occupancy
        public async Task<int> GetRoomOccupancyAsync(int roomId)
        {
            return await _context.Girls
                .CountAsync(x => x.RoomId == roomId && x.IsActive);
        }
    }
}
