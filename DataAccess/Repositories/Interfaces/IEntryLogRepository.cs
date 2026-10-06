using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IEntryLogRepository : IRepository<EntryLog>
    {
        Task<IEnumerable<EntryLog>> GetGirlsCurrentlyOutsideAsync();

        Task<IEnumerable<EntryLog>> GetTodayEntryLogsAsync();

        Task<IEnumerable<EntryLog>> GetMonthlyEntryLogsAsync(int month, int year);

        Task MarkGirlReturnAsync(int entryLogId);
    }
}
