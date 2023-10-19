using eSports.Backend.Domain.Players.Response;
using eSports.DAL.Interfaces;
using eSports.DAL.Repositories;
using eSports.Domain.Enum;
using eSports.Domain.Extensions;
using eSports.Domain.Players.Entity;
using eSports.Domain.Players.ViewModels;
using eSports.Domain.Teams.Entity;
using eSports.Domain.Teams.Filter;
using eSports.Domain.Teams.Response;
using eSports.Domain.Teams.ViewModels;
using eSports.Service.Players.Implementations;
using eSports.Service.Teams.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eSports.Service.Teams.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly IBaseRepository<TeamEntity> _teamRepository;
        private ILogger<PlayerService> _logger;

        public TeamService(IBaseRepository<TeamEntity> teamRepository, ILogger<PlayerService> logger) =>
                (_teamRepository, _logger) = (teamRepository, logger);

        public async Task<ITeamResponse<TeamEntity>> Create(CreateTeamViewModel model)
        {
            try
            {
                model.Validate();

                _logger.LogInformation($"Запрос на создание команды - {model.Name}");

                var team = await _teamRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == model.Name);

                if (team != null)
                {
                    return new TeamResponse<TeamEntity>()
                    {
                        Description = "Такая команда уже есть",
                        StatusCode = StatusCode.TeamAlreadyExists
                    };
                }

                team = new TeamEntity()
                {
                    Name = model.Name,
                    Country = model.Country,
                    Players = model.Players,
                };

                await _teamRepository.Create(team);

                _logger.LogInformation($"Команда создалась: {team.Name} {DateTime.Now}");
                return new TeamResponse<TeamEntity>()
                {
                    Description = "Команда создана",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[TeamService.Create]: {exception.Message}");
                return new TeamResponse<TeamEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<ITeamResponse<TeamEntity>> Delete(TeamViewModel model)
        {
            try
            {
                _logger.LogInformation($"Запрос на удаление команды - {model.Name}");

                var team = await _teamRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == model.Name);

                if (team == null)
                {
                    return new TeamResponse<TeamEntity>()
                    {
                        Description = "Такой команды нет",
                        StatusCode = StatusCode.TeamNotFound
                    };
                }

                await _teamRepository.Delete(team);

                _logger.LogInformation($"Команда удалилась: {team.Name} {DateTime.Now}");
                return new TeamResponse<TeamEntity>()
                {
                    Description = "Команда удалена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[TeamService.Delete]: {exception.Message}");
                return new TeamResponse<TeamEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<ITeamResponse<IEnumerable<TeamViewModel>>> GetAllTeams(TeamFilter filter)
        {
            try
            {
                var team = await _teamRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.Name),
                        x => x.Name.Contains(filter.Name))
                    .Select(x => new TeamViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Country = x.Country,
                        Players = x.Players
                    })
                    .ToListAsync();

                return new TeamResponse<IEnumerable<TeamViewModel>>()
                {
                    Data = team,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[TeamService.GetAllTeams]: {exception.Message}");
                return new TeamResponse<IEnumerable<TeamViewModel>>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<ITeamResponse<TeamEntity>> Update(TeamViewModel model)
        {
            try
            {
                _logger.LogInformation($"Запрос на изменение команды - {model.Name}");

                var team = await _teamRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.Id);

                if (team == null)
                {
                    return new TeamResponse<TeamEntity>()
                    {
                        Description = "Такая команда не найдена",
                        StatusCode = StatusCode.TeamNotFound
                    };
                }

                team = new TeamEntity()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Country = model.Country,
                    Players = model.Players
                };

                await _teamRepository.Update(team);

                _logger.LogInformation($"Команда {team.Name} изменена");
                return new TeamResponse<TeamEntity>()
                {
                    Description = $"Команда {team.Name} изменена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[TeamService.Update]: {exception.Message}");
                return new TeamResponse<TeamEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
