using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IEnumerable<Room>> GetRoomsByHostelAsync(int hostelId);

        Task<Room?> GetRoomWithGirlsAsync(int roomId);

        Task<IEnumerable<Room>> GetAvailableRoomsAsync(int hostelId);

        Task<int> GetRoomOccupancyAsync(int roomId);
    }
}
