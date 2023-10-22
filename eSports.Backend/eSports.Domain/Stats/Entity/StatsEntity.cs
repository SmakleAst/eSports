using eSports.Domain.Teams.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSports.Domain.Stats.Entity
{
    public class StatsEntity
    {
        public int Id { get; set; }
        public string FirstTeam { get; set; }
        public string SecondTeam { get; set; }
        public int FirstTeamScore { get; set; }
        public int SecondTeamScore { get; set; }
    }
}
