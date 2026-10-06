using Common.Enums;
using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IStaffRepository : IRepository<Staff>
    {
        Task<IEnumerable<Staff>> GetStaffByHostelAsync(int hostelId);

        Task<IEnumerable<Staff>> GetStaffByRoleAsync(StaffRole role);

        Task<Staff?> GetStaffWithHostelAsync(int staffId);
    }
}
