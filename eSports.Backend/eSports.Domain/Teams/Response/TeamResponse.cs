using eSports.Domain.Enum;
using eSports.Domain.Players.Entity;

namespace eSports.Domain.Teams.Response
{
    public class TeamResponse<T> : ITeamResponse<T>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public List<PlayerEntity> Players { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }

    public interface ITeamResponse<T>
    {
        string Name { get; }
        string Country { get; }
        List<PlayerEntity> Players { get; }
        public StatusCode StatusCode { get; }
        T Data { get; }
    }
}
