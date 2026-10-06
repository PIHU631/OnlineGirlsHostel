using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IGirlRepository : IRepository<Girl>
    {
        Task<IEnumerable<Girl>> GetGirlsByRoomAsync(int roomId);

        Task<IEnumerable<Girl>> GetGirlsByHostelAsync(int hostelId);

        Task<IEnumerable<Girl>> GetActiveGirlsAsync();

        Task<Girl?> GetGirlWithDetailsAsync(int girlId);
    }
}
