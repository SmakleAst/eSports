using eSports.Domain.Tournament.Entity;
using eSports.Domain.Tournament.Filter;
using eSports.Domain.Tournament.Response;
using eSports.Domain.Tournament.ViewModels;

namespace eSports.Service.Tournaments.Interfaces
{
    public interface ITournamentService
    {
        Task<ITournamentResponse<TournamentEntity>> Create(CreateTournamentViewModel model);

        Task<ITournamentResponse<TournamentEntity>> Delete(int id);

        Task<ITournamentResponse<TournamentEntity>> UpdateTournament(TournamentViewModel model);

        Task<ITournamentResponse<IEnumerable<TournamentViewModel>>> GetAllTournaments(TournamentFilter filter);

        Task<ITournamentResponse<TournamentViewModel>> GetTournament(int id);
    }
}
