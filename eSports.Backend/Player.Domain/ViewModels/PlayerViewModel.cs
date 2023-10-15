using System.ComponentModel.DataAnnotations;

namespace Players.Domain.ViewModels
{
    public class PlayerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Никнейм")]
        public string NickName { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Команда")]
        public string Team { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
