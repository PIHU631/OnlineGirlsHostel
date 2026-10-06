using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IMarketVisitRepository : IRepository<MarketVisit>
    {
        Task<IEnumerable<MarketVisit>> GetTodayMarketVisitsAsync();

        Task<IEnumerable<MarketVisit>> GetMonthlyMarketVisitsAsync(int month, int year);

        Task<IEnumerable<MarketVisit>> GetGirlMarketVisitsAsync(int girlId);

        Task<IEnumerable<MarketVisit>> GetGirlsCurrentlyInMarketAsync();

        Task MarkReturnAsync(int marketVisitId);
    }
}
