using eSports.Domain.Teams.Entity;

namespace eSports.Domain.Tournament.Entity
{
    public class TournamentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TeamEntity> Teams { get; set; }
    }
}
