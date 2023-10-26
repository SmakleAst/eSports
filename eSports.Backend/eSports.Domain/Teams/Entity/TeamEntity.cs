using eSports.Domain.Players.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSports.Domain.Teams.Entity
{
    public class TeamEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<PlayerEntity> Players { get; set; }
    }
}
