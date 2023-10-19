using System.ComponentModel.DataAnnotations;

namespace eSports.Domain.Stats.Filter
{
    public class StatsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Первая команда")]
        public string FirstTeam { get; set; }

        [Display(Name = "Вторая команда")]
        public string SecondTeam { get; set; }

        [Display(Name = "Винрейт")]
        public Tuple<int, int> Wins { get; set; }
    }
}
