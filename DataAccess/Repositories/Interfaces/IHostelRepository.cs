using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IHostelRepository : IRepository<Hostel>
    {
        Task<Hostel?> GetHostelWithRoomsAsync(int hostelId);

        Task<Hostel?> GetHostelWithStaffAsync(int hostelId);

        Task<IEnumerable<Hostel>> GetAllHostelsWithRoomsAsync();
    }
}
