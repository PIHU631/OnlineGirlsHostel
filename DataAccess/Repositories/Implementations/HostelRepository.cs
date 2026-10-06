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
    public class HostelRepository : IHostelRepository
    {
        private readonly GirlsHostelDbContext _context;

        public HostelRepository(GirlsHostelDbContext context)
        {
            _context = context;
        }

        // ======================
        // IRepository Methods
        // ======================

        public async Task<IEnumerable<Hostel>> GetAllAsync()
        {
            return await _context.Hostels
                .ToListAsync();
        }

        public async Task<Hostel?> GetByIdAsync(int id)
        {
            return await _context.Hostels
                .FirstOrDefaultAsync(x => x.HostelId == id);
        }

        public async Task AddAsync(Hostel entity)
        {
            await _context.Hostels.AddAsync(entity);
        }

        public void Update(Hostel entity)
        {
            _context.Hostels.Update(entity);
        }

        public void Remove(Hostel entity)
        {
            _context.Hostels.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // ======================
        // Custom Methods
        // ======================

        // Hostel with rooms
        public async Task<Hostel?> GetHostelWithRoomsAsync(int hostelId)
        {
            return await _context.Hostels
                .Include(x => x.Rooms)
                .FirstOrDefaultAsync(x => x.HostelId == hostelId);
        }

        // Hostel with staff
        public async Task<Hostel?> GetHostelWithStaffAsync(int hostelId)
        {
            return await _context.Hostels
                .Include(x => x.Staffs)
                .FirstOrDefaultAsync(x => x.HostelId == hostelId);
        }

        // All hostels with rooms
        public async Task<IEnumerable<Hostel>> GetAllHostelsWithRoomsAsync()
        {
            return await _context.Hostels
                .Include(x => x.Rooms)
                .ToListAsync();
        }
    }
}
