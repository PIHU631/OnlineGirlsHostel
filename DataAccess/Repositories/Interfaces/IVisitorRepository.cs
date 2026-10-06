using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IVisitorRepository : IRepository<Visitor>
    {
        Task<Visitor?> GetVisitorWithLogsAsync(int visitorId);

        Task<IEnumerable<Visitor>> SearchVisitorsAsync(string keyword);

        Task<Visitor?> GetVisitorByPhoneAsync(string phone);
    }
}
