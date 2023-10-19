using eSports.Domain.Players.Entity;

namespace eSports.Domain.Teams.Filter
{
    public class TeamFilter
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public List<PlayerEntity> Players { get; set; }
    }
}
