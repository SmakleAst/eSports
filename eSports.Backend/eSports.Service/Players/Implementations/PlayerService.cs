using eSports.Backend.Domain.Players.Response;
using eSports.DAL.Interfaces;
using eSports.Domain.Enum;
using eSports.Domain.Extensions;
using eSports.Domain.Players.Entity;
using eSports.Domain.Players.Filter;
using eSports.Domain.Players.ViewModels;
using eSports.Domain.Teams.Entity;
using eSports.Service.Players.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eSports.Service.Players.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly IBaseRepository<PlayerEntity> _playerRepository;
        private readonly IBaseRepository<TeamEntity> _teamRepository;
        private ILogger<PlayerService> _logger;

        public PlayerService(IBaseRepository<PlayerEntity> playerRepository,
            IBaseRepository<TeamEntity> teamRepository, ILogger<PlayerService> logger) =>
                (_playerRepository, _teamRepository, _logger) = (playerRepository, teamRepository, logger);

        public async Task<IPlayerResponse<PlayerEntity>> Create(CreatePlayerViewModel model)
        {
            try
            {
                model.Validate();

                _logger.LogInformation($"Запрос на создание игрока - {model.NickName}");

                var player = await _playerRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.NickName == model.NickName);

                if (player != null)
                {
                    return new PlayerResponse<PlayerEntity>()
                    {
                        Description = "Такой игрок уже есть",
                        StatusCode = StatusCode.PlayerAlreadyExists
                    };
                }

                var team = await _teamRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == model.Team);

                if (team == null)
                {
                    return new PlayerResponse<PlayerEntity>()
                    {
                        Description = "Такой команды нет",
                        StatusCode = StatusCode.TeamNotFound
                    };
                }

                player = new PlayerEntity()
                {
                    Name = model.Name,
                    NickName = model.NickName,
                    Age = model.Age,
                    TeamId = team.Id,
                    Team = team,
                    Description = model.Description
                };

                
                await _playerRepository.Create(player);

                _logger.LogInformation($"Игрок создался: {player.NickName} {DateTime.Now}");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = "Игрок создан",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[PlayerService.Create]: {exception.Message}");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IPlayerResponse<PlayerEntity>> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Запрос на удаление игрока - {id}");

                var player = await _playerRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (player == null)
                {
                    return new PlayerResponse<PlayerEntity>()
                    {
                        Description = "Такого игрока нет",
                        StatusCode = StatusCode.PlayerNotFound
                    };
                }

                await _playerRepository.Delete(player);

                _logger.LogInformation($"Игрок удалился: {player.NickName} {DateTime.Now}");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = "Игрок удален",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[PlayerService.Delete]: {exception.Message}");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IPlayerResponse<IEnumerable<PlayerViewModel>>> GetAllPlayers(PlayerFilter filter)
        {
            try
            {
                var player = await _playerRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.Name),
                        x => x.Name.Contains(filter.Name))
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.NickName),
                        x => x.NickName.Contains(filter.NickName))
                    .WhereIf(!string.IsNullOrWhiteSpace(filter.Team),
                        x => x.Team.Name.Contains(filter.Team))
                    .WhereIf(filter.Age > 10 && filter.Age < 100,
                        x => x.Age == filter.Age)
                    .Select(x => new PlayerViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        NickName = x.NickName,
                        Age = x.Age,
                        Team = x.Team.Name,
                        Description = x.Description,
                    })
                    .ToListAsync();

                return new PlayerResponse<IEnumerable<PlayerViewModel>>()
                {
                    Data = player,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[PlayerService.GetAllPlayers]: {exception.Message}");
                return new PlayerResponse<IEnumerable<PlayerViewModel>>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IPlayerResponse<PlayerEntity>> Update(PlayerViewModel model)
        {
            try
            {
                _logger.LogInformation($"Запрос на изменение игрока - {model.NickName}");

                var player = await _playerRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.Id);

                if (player == null)
                {
                    return new PlayerResponse<PlayerEntity>()
                    {
                        Description = "Такой игрок не найден",
                        StatusCode = StatusCode.PlayerNotFound
                    };
                }

                var team = await _teamRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Name == model.Name);

                if (team == null)
                {
                    return new PlayerResponse<PlayerEntity>()
                    {
                        Description = "Такой команды нет",
                        StatusCode = StatusCode.TeamNotFound
                    };
                }

                player = new PlayerEntity()
                {
                    Id = model.Id,
                    Name = model.Name,
                    NickName = model.NickName,
                    Age = model.Age,
                    Team = team,
                    Description = model.Description,
                };

                await _playerRepository.Update(player);

                _logger.LogInformation($"Игрок {player.NickName} изменен");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = $"Игрок {player.NickName} изменен",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[PlayerService.Update]: {exception.Message}");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IPlayerResponse<PlayerViewModel>> GetPlayer(int id)
        {
            try
            {
                _logger.LogInformation($"Запрос на получение игрока - {id}");

                var player = await _playerRepository.GetAll()
                    .Select(x => new PlayerViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        NickName = x.NickName,
                        Age = x.Age,
                        Team = x.Team.Name,
                        Description = x.Description,
                    })
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (player == null)
                {
                    return new PlayerResponse<PlayerViewModel>()
                    {
                        Description = "Такого игрока нет",
                        StatusCode = StatusCode.PlayerNotFound
                    };
                }

                return new PlayerResponse<PlayerViewModel>()
                {
                    Data = player,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[PlayerService.GetPlayer]: {exception.Message}");
                return new PlayerResponse<PlayerViewModel>()
                {
                    Description = $"{exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
