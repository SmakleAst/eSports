﻿using System.ComponentModel.DataAnnotations;

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
        public string Teams { get; set; }
    }
}
