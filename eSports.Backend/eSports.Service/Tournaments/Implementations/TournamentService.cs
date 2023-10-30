using eSports.DAL.Interfaces;
using eSports.Domain.Enum;
using eSports.Domain.Extensions;
using eSports.Domain.Teams.Entity;
using eSports.Domain.Teams.Response;
using eSports.Domain.Teams.ViewModels;
using eSports.Domain.Tournament.Entity;
using eSports.Domain.Tournament.Filter;
using eSports.Domain.Tournament.Response;
using eSports.Domain.Tournament.ViewModels;
using eSports.Service.Tournaments.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eSports.Service.Tournaments.Implementations
{
    public class TournamentService : ITournamentService
    {
        private readonly IBaseRepository<TournamentEntity> _tournamentRepository;
        private readonly IBaseRepository<TeamEntity> _teamRepository;
        private ILogger<TournamentService> _logger;

        public TournamentService(IBaseRepository<TournamentEntity> tournamentRepository,
            IBaseRepository<TeamEntity> teamRepository, ILogger<TournamentService> logger) =>
                (_tournamentRepository, _teamRepository, _logger) =
                    (tournamentRepository, teamRepository, logger);

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

        public async Task<ITournamentResponse<TournamentEntity>> UpdateTournament(TournamentViewModel model)
        {
            //TODO: Реализовать механизм симуляции стадии турнира
            throw new NotImplementedException();
        }
    }
}
