using eSports.Domain.Enum;
using eSports.Domain.Teams.Entity;

namespace eSports.Domain.Stats.Filter
{
    public class StatsResponse<T> : IStatsResponse<T>
    {
        public TeamEntity FirstTeam { get; set; }
        public TeamEntity SecondTeam { get; set; }
        public Tuple<int, int> Wins { get; set; }
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }

    public interface IStatsResponse<T>
    {
        TeamEntity FirstTeam { get; }
        TeamEntity SecondTeam { get; }
        Tuple<int, int> Wins { get; }
        string Description { get; }
        public StatusCode StatusCode { get; }
        T Data { get; }
    }
}
