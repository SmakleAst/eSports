using eSports.Domain.Teams.Entity;
using System.ComponentModel.DataAnnotations;

namespace eSports.Domain.Stats.Filter
{
    public class StatsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Первая команда")]
        public TeamEntity FirstTeam { get; set; }

        [Display(Name = "Вторая команда")]
        public TeamEntity SecondTeam { get; set; }

        [Display(Name = "Винрейт")]
        public double Winrate { get; set; }
    }
}
