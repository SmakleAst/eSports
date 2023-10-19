using eSports.Domain.Players.Entity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace eSports.Domain.Teams.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название команды")]
        public string Name { get; set; }

        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Display(Name = "Игроки:")]
        public List<PlayerEntity> Players { get; set; }
    }
}
