using Players.Domain.Entity;
using Players.Domain.Filter;
using Players.Domain.Response;
using Players.Domain.ViewModels;

namespace Player.Service.Interfaces
{
    public interface IPlayerService
    {
        Task<IPlayerResponse<PlayerEntity>> Create(CreatePlayerViewModel model);

        Task<IPlayerResponse<PlayerEntity>> Delete(PlayerViewModel model);

        Task<IPlayerResponse<PlayerEntity>> Update(PlayerViewModel model);

        Task<IPlayerResponse<IEnumerable<PlayerViewModel>>> GetAllPlayers(PlayerFilter filter);
    }
}
