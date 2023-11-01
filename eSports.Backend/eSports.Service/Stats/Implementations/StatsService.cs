using eSports.DAL.Interfaces;
using eSports.Domain.Enum;
using eSports.Domain.Extensions;
using eSports.Domain.Stats.Entity;
using eSports.Domain.Stats.Filter;
using eSports.Domain.Stats.ViewModels;
using eSports.Domain.Teams.Entity;
using eSports.Service.Stats.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eSports.Service.Stats.Implementations
{
    public class StatsService : IStatsService
    {
        private readonly IBaseRepository<StatsEntity> _statsRepository;
        private readonly IBaseRepository<TeamEntity> _teamRepository;
        private ILogger<StatsService> _logger;

        public StatsService(IBaseRepository<StatsEntity> statsRepository,
            IBaseRepository<TeamEntity> teamRepository, ILogger<StatsService> logger) =>
                (_statsRepository, _teamRepository, _logger) = (statsRepository, teamRepository, logger);

        public async Task<IStatsResponse<StatsEntity>> Create(StatsViewModel model)
        {
            try
            {
                _logger.LogInformation($"Запрос на создание поля статистики" +
                    $"- {model.FirstTeam} против {model.SecondTeam}");

                var stat = await _statsRepository.GetAll()
                    .FirstOrDefaultAsync(x => (x.FirstTeam.Equals(model.FirstTeam) &&
                                            x.SecondTeam.Equals(model.SecondTeam)) ||
                                            (x.FirstTeam.Equals(model.SecondTeam) &&
                                            x.SecondTeam.Equals(model.FirstTeam)));

                if (stat != null)
                {
                    return new StatsResponse<StatsEntity>()
                    {
                        Description = "Статистика этих команд уже есть",
                        StatusCode = StatusCode.StatsAlreadyExists
                    };
                }

                var dbFirstTeam = await _teamRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == model.FirstTeam);

                var dbSecondTeam = await _teamRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == model.SecondTeam);

                if (dbFirstTeam == null || dbSecondTeam == null)
                {
                    return new StatsResponse<StatsEntity>()
                    {
                        Description = "Такой команды нет",
                        StatusCode = StatusCode.TeamNotFound
                    };
                }

                await _teamRepository.Attach(dbFirstTeam);
                await _teamRepository.Attach(dbSecondTeam);

                stat = new StatsEntity()
                {
                    FirstTeam = dbFirstTeam.Name,
                    SecondTeam = dbSecondTeam.Name,
                    FirstTeamScore = model.FirstTeamScore,
                    SecondTeamScore = model.SecondTeamScore
                };

                await _statsRepository.Create(stat);

                _logger.LogInformation($"Статистика создалась: {stat.FirstTeam} против {stat.SecondTeam}");
                return new StatsResponse<StatsEntity>()
                {
                    Description = "Статистика создана",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[StatsService.Create]: {exception.Message}");
                return new StatsResponse<StatsEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IStatsResponse<StatsEntity>> Delete(StatsViewModel model)
        {
            try
            {
                _logger.LogInformation($"Запрос на удаление статистики" +
                    $"- {model.FirstTeam} против {model.SecondTeam}");

                var stat = await _statsRepository.GetAll()
                    .FirstOrDefaultAsync(x => (x.FirstTeam.Equals(model.FirstTeam) &&
                                            x.SecondTeam.Equals(model.SecondTeam)) ||
                                            (x.FirstTeam.Equals(model.SecondTeam) &&
                                            x.SecondTeam.Equals(model.FirstTeam)));

                if (stat == null)
                {
                    return new StatsResponse<StatsEntity>()
                    {
                        Description = "Такой статистики нет",
                        StatusCode = StatusCode.StatsNotFound
                    };
                }

                await _statsRepository.Delete(stat);

                _logger.LogInformation($"Статистика удалилась: {stat.FirstTeam}" +
                    $"против {stat.SecondTeam}");
                return new StatsResponse<StatsEntity>()
                {
                    Description = "Статистика удалена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[StatsService.Delete]: {exception.Message}");
                return new StatsResponse<StatsEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IStatsResponse<IEnumerable<StatsViewModel>>> GetAllStats(StatsFilter filter)
        {
            try
            {
                var stat = await _statsRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.Team),
                        x => x.FirstTeam.Contains(filter.Team) ||
                            x.SecondTeam.Contains(filter.Team))
                    .Select(x => new StatsViewModel()
                    {
                        Id = x.Id,
                        FirstTeam = x.FirstTeam,
                        SecondTeam = x.SecondTeam,
                        FirstTeamScore = x.FirstTeamScore,
                        SecondTeamScore = x.SecondTeamScore
                    })
                    .ToListAsync();

                return new StatsResponse<IEnumerable<StatsViewModel>>()
                {
                    Data = stat,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[StatsService.GetAllStats]: {exception.Message}");
                return new StatsResponse<IEnumerable<StatsViewModel>>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IStatsResponse<StatsEntity>> Update(ResultMatchViewModel model)
        {
            try
            {
                _logger.LogInformation($"Запрос на изменение статистики - {model.FirstTeam}" +
                    $"против {model.SecondTeam}");

                var stat = await _statsRepository.GetAll()
                    .FirstOrDefaultAsync(x => (x.FirstTeam.Equals(model.FirstTeam) &&
                            x.SecondTeam.Equals(model.SecondTeam)) ||
                            (x.FirstTeam.Equals(model.SecondTeam) &&
                            x.SecondTeam.Equals(model.FirstTeam)));

                if (stat == null)
                {
                    return await Create(new StatsViewModel
                    {
                        FirstTeam = model.FirstTeam,
                        SecondTeam = model.SecondTeam,
                        FirstTeamScore = model.Winner.Equals(model.FirstTeam) ? 1 : 0,
                        SecondTeamScore = model.Winner.Equals(model.SecondTeam) ? 1 : 0
                    });
                }

                stat = new StatsEntity
                {
                    Id = stat.Id,
                    FirstTeam = stat.FirstTeam,
                    SecondTeam = stat.SecondTeam,
                    FirstTeamScore = model.Winner.Equals(stat.FirstTeam) ?
                        stat.FirstTeamScore += 1 : stat.FirstTeamScore,
                    SecondTeamScore = model.Winner.Equals(stat.SecondTeam) ?
                        stat.SecondTeamScore += 1 : stat.SecondTeamScore
                };

                await _statsRepository.Update(stat);

                _logger.LogInformation($"Статистика {stat.FirstTeam} против" +
                    $"{stat.SecondTeam} изменена");

                return new StatsResponse<StatsEntity>()
                {
                    Description = $"Статистика {stat.FirstTeam} против" +
                        $"{stat.SecondTeam} изменена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[StatsService.Update]: {exception.Message}");
                return new StatsResponse<StatsEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
