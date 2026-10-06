using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRoomHistoryRepository : IRepository<RoomHistory>
    {
        Task<IEnumerable<RoomHistory>> GetGirlRoomHistoryAsync(int girlId);

        Task<RoomHistory?> GetCurrentRoomAsync(int girlId);

        Task<IEnumerable<RoomHistory>> GetRoomHistoryByRoomAsync(int roomId);

        Task EndCurrentRoomAsync(int girlId);
    }
}
