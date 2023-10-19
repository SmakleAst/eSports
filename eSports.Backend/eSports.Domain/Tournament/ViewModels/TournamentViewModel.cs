using eSports.Domain.Teams.Entity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace eSports.Domain.Tournament.ViewModels
{
    public class TournamentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название турнира")]
        public string Name { get; set; }

        [Display(Name = "Описание турнира")]
        public string Description { get; set; }

        [Display(Name = "Участники:")]
        public List<TeamEntity> Teams { get; set; }
    }
}
