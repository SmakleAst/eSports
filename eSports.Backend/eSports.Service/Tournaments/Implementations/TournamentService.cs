using eSports.DAL.Interfaces;
using eSports.Domain.Enum;
using eSports.Domain.Extensions;
using eSports.Domain.Stats.Entity;
using eSports.Domain.Stats.ViewModels;
using eSports.Domain.Teams.Entity;
using eSports.Domain.Tournament.Entity;
using eSports.Domain.Tournament.Filter;
using eSports.Domain.Tournament.Response;
using eSports.Domain.Tournament.ViewModels;
using eSports.Service.Tournaments.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace eSports.Service.Tournaments.Implementations
{
    public class TournamentService : ITournamentService
    {
        private readonly IBaseRepository<TournamentEntity> _tournamentRepository;
        private readonly IBaseRepository<TeamEntity> _teamRepository;
        private readonly IBaseRepository<StatsEntity> _statRepository;
        private ILogger<TournamentService> _logger;
        private static Random _randomWinner = new Random();

        public TournamentService(IBaseRepository<TournamentEntity> tournamentRepository,
            IBaseRepository<TeamEntity> teamRepository, IBaseRepository<StatsEntity> statRepository,
                ILogger<TournamentService> logger) =>
                    (_tournamentRepository, _teamRepository, _statRepository, _logger) =
                        (tournamentRepository, teamRepository, statRepository, logger);

        public async Task<ITournamentResponse<TournamentEntity>> Create(CreateTournamentViewModel model)
        {
            try
            {
                _logger.LogInformation($"Запрос на создание турнира - {model.Name}");

                var tournament = await _tournamentRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == model.Name);

                if (tournament != null)
                {
                    return new TournamentResponse<TournamentEntity>()
                    {
                        Description = "Такой турнир уже есть",
                        StatusCode = StatusCode.TournamentAlreadyExists
                    };
                }

                if (model.Teams.Count % 2 != 0)
                {
                    return new TournamentResponse<TournamentEntity>()
                    {
                        Description = "Для создания турнира должно быть четное количество участвующих команд",
                        StatusCode = StatusCode.InvalidTournamentTeamsCount
                    };
                }

                var existingTeams = _teamRepository.GetAll()
                                            .Where(x => model.Teams.Contains(x.Name))
                                            .ToList();

                foreach (var team in existingTeams)
                {
                    await _teamRepository.Attach(team);
                }

                tournament = new TournamentEntity()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Teams = existingTeams,
                };

                await _tournamentRepository.Create(tournament);

                _logger.LogInformation($"Турнир создался: {tournament.Name} {DateTime.Now}");
                return new TournamentResponse<TournamentEntity>()
                {
                    Description = "Турнир создан",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[TournamentService.Create]: {exception.Message}");
                return new TournamentResponse<TournamentEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<ITournamentResponse<TournamentEntity>> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Запрос на удаление турнира - {id}");

                var tournament = await _tournamentRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (tournament == null)
                {
                    return new TournamentResponse<TournamentEntity>()
                    {
                        Description = "Такого туринра нет",
                        StatusCode = StatusCode.TournamentNotFound
                    };
                }

                await _tournamentRepository.Delete(tournament);

                _logger.LogInformation($"Турнир удалился: {tournament.Name} {DateTime.Now}");
                return new TournamentResponse<TournamentEntity>()
                {
                    Description = "Турнир удален",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[TournamentService.Delete]: {exception.Message}");
                return new TournamentResponse<TournamentEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<ITournamentResponse<IEnumerable<TournamentViewModel>>> GetAllTournaments(TournamentFilter filter)
        {
            try
            {
                var tournament = await _tournamentRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.Name),
                        x => x.Name.Contains(filter.Name))
                    .Select(x => new TournamentViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Teams = string.Join(", ", x.Teams.Select(t => t.Name))
                    })
                    .ToListAsync();

                return new TournamentResponse<IEnumerable<TournamentViewModel>>()
                {
                    Data = tournament,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[TournamentService.GetAllTournaments]: {exception.Message}");
                return new TournamentResponse<IEnumerable<TournamentViewModel>>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<ITournamentResponse<TournamentViewModel>> GetTournament(int id)
        {
            try
            {
                _logger.LogInformation($"Запрос на получение турнира - {id}");

                var tournament = await _tournamentRepository.GetAll()
                    .Select(x => new TournamentViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Teams = string.Join(", ", x.Teams.Select(p => p.Name)),
                    })
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (tournament == null)
                {
                    return new TournamentResponse<TournamentViewModel>()
                    {
                        Description = "Такого турнира нет",
                        StatusCode = StatusCode.PlayerNotFound
                    };
                }

                return new TournamentResponse<TournamentViewModel>()
                {
                    Data = tournament,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[TournamentService.GetTournament]: {exception.Message}");
                return new TournamentResponse<TournamentViewModel>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<ITournamentResponse<TournamentViewModel>> SimulateTournamentStage(int id)
        {
            try
            {
                _logger.LogInformation($"Запрос на симуляцию стадии турнира - {id}");

                var tournament = await _tournamentRepository.GetAll()
                    .Include(c => c.Teams)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (tournament == null)
                {
                    return new TournamentResponse<TournamentViewModel>()
                    {
                        Description = "Такого турнира нет",
                        StatusCode = StatusCode.PlayerNotFound
                    };
                }

                foreach (var team in tournament.Teams)
                {
                    await _teamRepository.Attach(team);
                }

                tournament.Teams.Shuffle();

                for (int k = 0; k < tournament.Teams.Count / 2; k++)
                {
                    for (int i = 0, j = 1; j < tournament.Teams.Count; i++, j++)
                    {
                        if (_randomWinner.Next(0, 2) == 0)
                        {
                            var resultMatch = new ResultMatchViewModel()
                            {
                                FirstTeam = tournament.Teams[i].Name,
                                SecondTeam = tournament.Teams[j].Name,
                                Winner = tournament.Teams[i].Name
                            };

                            var json = JsonConvert.SerializeObject(resultMatch);

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri("https://localhost:7126/Stats/UpdateStats");

                                var content = new StringContent(json, Encoding.UTF8, "application/json");
                                var result = await client
                                    .PostAsync("https://localhost:7126/Stats/UpdateStats", content);

                                if (result.IsSuccessStatusCode)
                                {
                                    _logger.LogInformation
                                        ($"Статистика записана -" +
                                        $"{tournament.Teams[i].Name} против" +
                                        $"{tournament.Teams[j].Name}");
                                }
                            }

                            tournament.Teams.RemoveAt(j);
                        }
                        else
                        {
                            var resultMatch = new ResultMatchViewModel()
                            {
                                FirstTeam = tournament.Teams[i].Name,
                                SecondTeam = tournament.Teams[j].Name,
                                Winner = tournament.Teams[j].Name
                            };

                            var json = JsonConvert.SerializeObject(resultMatch);

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri("https://localhost:7126/Stats/UpdateStats");

                                var content = new StringContent(json, Encoding.UTF8, "application/json");
                                var result = await client
                                    .PostAsync("https://localhost:7126/Stats/UpdateStats",content);

                                if (result.IsSuccessStatusCode)
                                {
                                    _logger.LogInformation
                                        ($"Статистика записана - " +
                                        $"{tournament.Teams[i].Name} против" +
                                        $"{tournament.Teams[j].Name}");
                                }
                            }

                            tournament.Teams.RemoveAt(i);
                        }
                    }
                }

                await _tournamentRepository.Update(tournament);

                return new TournamentResponse<TournamentViewModel>()
                {
                    Data = new TournamentViewModel {
                        Id = tournament.Id,
                        Name = tournament.Name,
                        Description = tournament.Description,
                        Teams = string.Join(", ", tournament.Teams.Select(p => p.Name)),
                    },
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[TournamentService.GetTournament]: {exception.Message}");
                return new TournamentResponse<TournamentViewModel>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
