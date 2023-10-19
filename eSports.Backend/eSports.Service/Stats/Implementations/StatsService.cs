using eSports.DAL.Interfaces;
using eSports.Domain.Players.ViewModels;
using eSports.Domain.Stats.Entity;
using eSports.Domain.Stats.Filter;
using eSports.Service.Stats.Interfaces;
using Microsoft.Extensions.Logging;

namespace eSports.Service.Stats.Implementations
{
    public class StatsService : IStatsService
    {
        private readonly IBaseRepository<StatsEntity> _statsRepository;
        private ILogger<StatsService> _logger;

        public StatsService(IBaseRepository<StatsEntity> statsRepository,
            ILogger<StatsService> logger) => (_statsRepository, _logger) = (statsRepository, logger);

        public Task<IStatsResponse<StatsEntity>> Create(StatsViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IStatsResponse<StatsEntity>> Delete(StatsViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IStatsResponse<IEnumerable<StatsViewModel>>> GetAllStats(StatsFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<IStatsResponse<StatsEntity>> Update(PlayerViewModel firstTeam, PlayerViewModel secondTeam)
        {
            throw new NotImplementedException();
        }
    }
}
