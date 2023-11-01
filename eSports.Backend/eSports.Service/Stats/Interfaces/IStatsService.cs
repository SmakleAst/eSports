using eSports.Domain.Stats.Entity;
using eSports.Domain.Stats.Filter;
using eSports.Domain.Stats.ViewModels;

namespace eSports.Service.Stats.Interfaces
{
    public interface IStatsService
    {
        Task<IStatsResponse<StatsEntity>> Create(StatsViewModel model);

        Task<IStatsResponse<StatsEntity>> Delete(StatsViewModel model);

        Task<IStatsResponse<StatsEntity>> Update(ResultMatchViewModel model);

        Task<IStatsResponse<IEnumerable<StatsViewModel>>> GetAllStats(StatsFilter filter);
    }
}
