using Common.Enums;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class StaffRepository : IStaffRepository
    {
        private readonly GirlsHostelDbContext _context;

        public StaffRepository(GirlsHostelDbContext context)
        {
            _context = context;
        }

        // ======================
        // IRepository Methods
        // ======================

        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            return await _context.Staffs
                .Include(x => x.Hostel)
                .ToListAsync();
        }

        public async Task<Staff?> GetByIdAsync(int id)
        {
            return await _context.Staffs
                .Include(x => x.Hostel)
                .FirstOrDefaultAsync(x => x.StaffId == id);
        }

        public async Task AddAsync(Staff entity)
        {
            await _context.Staffs.AddAsync(entity);
        }

        public void Update(Staff entity)
        {
            _context.Staffs.Update(entity);
        }

        public void Remove(Staff entity)
        {
            _context.Staffs.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // ======================
        // Custom Methods
        // ======================

        // Staff by hostel
        public async Task<IEnumerable<Staff>> GetStaffByHostelAsync(int hostelId)
        {
            return await _context.Staffs
                .Where(x => x.HostelId == hostelId)
                .ToListAsync();
        }

        // Staff by role (warden, cook etc)
        public async Task<IEnumerable<Staff>> GetStaffByRoleAsync(StaffRole role)
        {
            return await _context.Staffs
                .Where(x => x.Role == role)
                .ToListAsync();
        }

        // Staff with hostel details
        public async Task<Staff?> GetStaffWithHostelAsync(int staffId)
        {
            return await _context.Staffs
                .Include(x => x.Hostel)
                .FirstOrDefaultAsync(x => x.StaffId == staffId);
        }
    }
}
