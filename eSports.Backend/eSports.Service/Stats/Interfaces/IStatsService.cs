using eSports.Domain.Stats.Entity;
using eSports.Domain.Stats.Filter;
using eSports.Domain.Teams.ViewModels;

namespace eSports.Service.Stats.Interfaces
{
    public interface IStatsService
    {
        Task<IStatsResponse<StatsEntity>> Create(TeamViewModel firstTeam, TeamViewModel secondTeam);

        Task<IStatsResponse<StatsEntity>> Delete(StatsViewModel model);

        Task<IStatsResponse<StatsEntity>> Update(TeamViewModel firstTeam,
            TeamViewModel secondTeam, bool isFirstTeamWin);

        Task<IStatsResponse<IEnumerable<StatsViewModel>>> GetAllStats(StatsFilter filter);
    }
}
