using eSports.Domain.Players.Entity;
using eSports.Domain.Tournament.Entity;

namespace eSports.Domain.Teams.Entity
{
    public class TeamEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<PlayerEntity> Players { get; set; }
        public List<TournamentEntity> Tournaments { get; set; }
    }
}
