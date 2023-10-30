using eSports.Domain.Players.Entity;
using eSports.Domain.Tournament.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSports.Domain.Teams.Entity
{
    public class TeamEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<PlayerEntity> Players { get; set; }
        public List<TournamentEntity > Tournaments { get; set; }
    }
}
