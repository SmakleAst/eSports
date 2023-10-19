using eSports.Backend.Domain.Players.Response;
using eSports.DAL.Interfaces;
using eSports.Domain.Enum;
using eSports.Domain.Extensions;
using eSports.Domain.Stats.Entity;
using eSports.Domain.Stats.Filter;
using eSports.Domain.Teams.Entity;
using eSports.Domain.Teams.ViewModels;
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
                                IBaseRepository<TeamEntity> teamRepository,
                                ILogger<StatsService> logger) =>
                            (_statsRepository, _teamRepository, _logger) =
                            (statsRepository, teamRepository, logger);

        public async Task<IStatsResponse<StatsEntity>> Create(TeamViewModel firstTeam, TeamViewModel secondTeam)
        {
            try
            {
                _logger.LogInformation($"Запрос на создание поля статистики" +
                    $"- {firstTeam.Name} против {secondTeam.Name}");

                var stat = await _statsRepository.GetAll()
                    .FirstOrDefaultAsync(x => (x.FirstTeam.Name.Equals(firstTeam.Name) &&
                                            x.SecondTeam.Name.Equals(secondTeam.Name)) ||
                                            (x.FirstTeam.Name.Equals(secondTeam.Name) &&
                                            x.SecondTeam.Name.Equals(firstTeam.Name)));

                if (stat != null)
                {
                    return new StatsResponse<StatsEntity>()
                    {
                        Description = "Статистика этих команд уже есть",
                        StatusCode = StatusCode.StatsAlreadyExists
                    };
                }

                var dbFirstTeam = await _teamRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == firstTeam.Name);

                var dbSecondTeam = await _teamRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == firstTeam.Name);

                if (dbFirstTeam == null || dbSecondTeam == null)
                {
                    return new StatsResponse<StatsEntity>()
                    {
                        Description = "Такой команды нет",
                        StatusCode = StatusCode.TeamNotFound
                    };
                }

                stat = new StatsEntity()
                {
                    FirstTeam = dbFirstTeam,
                    SecondTeam = dbSecondTeam,
                    Wins = new Tuple<int, int>(0, 0),
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
                    .FirstOrDefaultAsync(x => (x.FirstTeam.Name.Equals(model.FirstTeam) &&
                                            x.SecondTeam.Name.Equals(model.SecondTeam)) ||
                                            (x.FirstTeam.Name.Equals(model.SecondTeam) &&
                                            x.SecondTeam.Name.Equals(model.FirstTeam)));

                if (stat == null)
                {
                    return new StatsResponse<StatsEntity>()
                    {
                        Description = "Такой статистики нет",
                        StatusCode = StatusCode.StatsNotFound
                    };
                }

                await _statsRepository.Delete(stat);

                _logger.LogInformation($"Статистика удалилась: {stat.FirstTeam.Name}" +
                    $"против {stat.SecondTeam.Name}");
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
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.FirstTeam),
                        x => x.FirstTeam.Name.Contains(filter.FirstTeam))
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.SecondTeam),
                        x => x.SecondTeam.Name.Contains(filter.SecondTeam))
                    .Select(s => new StatsViewModel()
                    {
                        FirstTeam = s.FirstTeam.Name,
                        SecondTeam = s.SecondTeam.Name,
                        Wins = s.Wins,
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

        public async Task<IStatsResponse<StatsEntity>> Update(TeamViewModel firstTeam,
            TeamViewModel secondTeam, bool isFirstTeamWin)
        {
            try
            {
                _logger.LogInformation($"Запрос на изменение статистики - {firstTeam.Name}" +
                    $"против {secondTeam.Name}");

                var stat = await _statsRepository.GetAll()
                    .FirstOrDefaultAsync(x => (x.FirstTeam.Name.Equals(firstTeam.Name) &&
                            x.SecondTeam.Name.Equals(secondTeam.Name)) ||
                            (x.FirstTeam.Name.Equals(secondTeam.Name) &&
                            x.SecondTeam.Name.Equals(firstTeam.Name)));

                if (stat == null)
                {
                    return new StatsResponse<StatsEntity>()
                    {
                        Description = "Такая статистика не найдена",
                        StatusCode = StatusCode.StatsNotFound
                    };
                }

                if (isFirstTeamWin)
                {
                    stat.Wins = new Tuple<int, int>(stat.Wins.Item1 + 1, stat.Wins.Item2);
                }
                else
                {
                    stat.Wins = new Tuple<int, int>(stat.Wins.Item1, stat.Wins.Item2 + 1);
                }

                await _statsRepository.Update(stat);

                _logger.LogInformation($"Статистика {stat.FirstTeam.Name} против" +
                    $"{stat.SecondTeam.Name} изменена");
                return new StatsResponse<StatsEntity>()
                {
                    Description = $"Статистика {stat.FirstTeam.Name} против" +
                        $"{stat.SecondTeam.Name} изменена",
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
