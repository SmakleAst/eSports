using eSports.DAL.Interfaces;
using eSports.Domain.Enum;
using eSports.Domain.Extensions;
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
        private ILogger<TournamentService> _logger;

        public TournamentService(IBaseRepository<TournamentEntity> tournamentRepository,
            ILogger<TournamentService> logger) =>
                (_tournamentRepository, _logger) = (tournamentRepository, logger);

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

                tournament = new TournamentEntity()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Teams = model.Teams
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

        public async Task<ITournamentResponse<TournamentEntity>> Delete(TournamentViewModel model)
        {
            try
            {
                _logger.LogInformation($"Запрос на удаление турнира - {model.Name}");

                var tournament = await _tournamentRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == model.Name);

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
                    Description = "Команда удалена",
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
                        Teams = x.Teams
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

        public async Task<ITournamentResponse<TournamentEntity>> UpdateTournament(TournamentViewModel model)
        {
            //TODO: Реализовать механизм симуляции стадии турнира
            throw new NotImplementedException();
        }
    }
}
