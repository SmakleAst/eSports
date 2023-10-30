using System.ComponentModel.DataAnnotations;

namespace eSports.Domain.Teams.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название команды")]
        public string Name { get; set; }

        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Display(Name = "Игроки: ")]
        public string Players { get; set; }

        [Display(Name = "Турниры: ")]
        public string Tournaments { get; set; }
    }
}
