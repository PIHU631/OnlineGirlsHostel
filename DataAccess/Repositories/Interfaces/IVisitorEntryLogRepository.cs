using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IVisitorEntryLogRepository : IRepository<VisitorEntryLog>
    {
        Task<IEnumerable<VisitorEntryLog>> GetTodayVisitsAsync();

        Task<IEnumerable<VisitorEntryLog>> GetGirlVisitsAsync(int girlId);

        Task<IEnumerable<VisitorEntryLog>> GetVisitorHistoryAsync(int visitorId);

        Task<IEnumerable<VisitorEntryLog>> GetVisitorsCurrentlyInsideAsync();

        Task MarkVisitorExitAsync(int visitorEntryLogId);
    }
}
