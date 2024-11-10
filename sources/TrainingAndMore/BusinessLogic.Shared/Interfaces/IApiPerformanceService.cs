using Shared.Models.Administration;
namespace BusinessLogic.Shared.Interfaces
{
    public interface IApiPerformanceService : IDisposable
    {
        Task<List<ApiPerformanceStatistics>?> GetApiPerformance();
    }
}
