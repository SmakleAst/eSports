using eSports.Domain.Teams.Entity;

namespace eSports.Domain.Stats.Entity
{
    public class StatsEntity
    {
        public int Id { get; set; }
        public TeamEntity FirstTeam { get; set; }
        public TeamEntity SecondTeam { get; set; }
        public Tuple<int, int> Wins { get; set; }
    }
}
