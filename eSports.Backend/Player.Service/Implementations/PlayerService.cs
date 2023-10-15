using eSports.Backend.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Player.Service.Interfaces;
using Players.Domain.Entity;
using Players.Domain.Filter;
using Players.Domain.Response;
using Players.Domain.ViewModels;
using Extensions.Domain;
using System.Linq;

namespace Player.Service.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly IBaseRepository<PlayerEntity> _playerRepository;
        private ILogger<PlayerService> _logger;

        public PlayerService(IBaseRepository<PlayerEntity> playerRepository,
            ILogger<PlayerEntity> logger) => (_playerRepository, _logger) = (playerRepository, logger);

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
                        //StatusCode = StatusCode.ComputerAlreadyExists
                    };
                }

                player = new PlayerEntity()
                {
                    Name = model.Name,
                    NickName = model.NickName,
                    Age = model.Age,
                    Team = model.Team,
                    Description = model.Description
                };

                await _playerRepository.Create(player);

                _logger.LogInformation($"Игрок создался: {player.NickName} {DateTime.Now}");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = "Игрок создан",
                    //StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[PlayerService.Create]: {exception.Message}");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = $"{exception.Message}",
                    //StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IPlayerResponse<PlayerEntity>> Delete(PlayerViewModel model)
        {
            try
            {
                _logger.LogInformation($"Запрос на удаление игрока - {model.NickName}");

                var player = await _playerRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.NickName == model.NickName);

                if (player == null)
                {
                    return new PlayerResponse<PlayerEntity>()
                    {
                        Description = "Такого игрока нет",
                        //StatusCode = StatusCode.ComputerAlreadyExists
                    };
                }

                await _playerRepository.Delete(player);

                _logger.LogInformation($"Игрок удалился: {player.NickName} {DateTime.Now}");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = "Компьютер удален",
                    //StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[ComputerService.Delete]: {exception.Message}");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = $"{exception.Message}",
                    //StatusCode = StatusCode.InternalServerError
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
                        x => x.Team.Contains(filter.Team))
                    .WhereIf(filter.Age > 10 && filter.Age < 100,
                        x => x.Age == filter.Age)
                    .Select(x => new PlayerViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        NickName = x.NickName,
                        Age = x.Age,
                        Team = x.Name,
                        Description = x.Description,
                    })
                    .ToListAsync();

                return new PlayerResponse<IEnumerable<PlayerViewModel>>()
                {
                    Data = player,
                    //StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[PlayerService.GetComputers]: {exception.Message}");
                return new PlayerResponse<IEnumerable<PlayerViewModel>>()
                {
                    Description = $"{exception.Message}",
                    //StatusCode = StatusCode.InternalServerError
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
                        //StatusCode = StatusCode.ComputerNotFound
                    };
                }

                player = new PlayerEntity()
                {
                    Id = model.Id,
                    Name = model.Name,
                    NickName = model.NickName,
                    Age = model.Age,
                    Team = model.Name,
                    Description = model.Description,
                };

                await _playerRepository.Update(player);

                _logger.LogInformation($"Игрок {player.NickName} изменен");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = $"Игрок {player.NickName} изменен",
                    //StatusCode = StatusCode.Ok
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[PlayerService.Update]: {exception.Message}");
                return new PlayerResponse<PlayerEntity>()
                {
                    Description = $"{exception.Message}",
                    //StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
