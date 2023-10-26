using eSports.Backend.Domain.Players.Response;
using eSports.Domain.Players.Entity;
using eSports.Domain.Players.Filter;
using eSports.Domain.Players.ViewModels;

namespace eSports.Service.Players.Interfaces
{
    public interface IPlayerService
    {
        Task<IPlayerResponse<PlayerEntity>> Create(CreatePlayerViewModel model);

        Task<IPlayerResponse<PlayerEntity>> Delete(int id);

        Task<IPlayerResponse<PlayerEntity>> Update(PlayerViewModel model);

        Task<IPlayerResponse<IEnumerable<PlayerViewModel>>> GetAllPlayers(PlayerFilter filter);
    }
}
