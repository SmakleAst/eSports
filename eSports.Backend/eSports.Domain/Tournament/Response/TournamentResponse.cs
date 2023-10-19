using eSports.Domain.Enum;
using eSports.Domain.Teams.Entity;

namespace eSports.Domain.Tournament.Response
{
    public class TournamentResponse<T> : ITournamentResponse<T>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TeamEntity> Teams { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }

    public interface ITournamentResponse<T>
    {
        string Name { get; }
        string Description { get; }
        List<TeamEntity> Teams { get; }
        public StatusCode StatusCode { get; }
        T Data { get; }
    }
}
