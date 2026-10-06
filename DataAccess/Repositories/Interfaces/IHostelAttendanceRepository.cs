using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IHostelAttendanceRepository : IRepository<HostelAttendance>
    {
        Task<IEnumerable<HostelAttendance>> GetTodayAttendanceAsync();

        Task<IEnumerable<HostelAttendance>> GetMonthlyAttendanceAsync(int month, int year);

        Task<IEnumerable<HostelAttendance>> GetGirlAttendanceAsync(int girlId);

        Task<IEnumerable<HostelAttendance>> GetAbsentGirlsAsync(DateTime date);

        Task<bool> HasGirlMarkedAttendanceAsync(int girlId, DateTime date);
    }
}
