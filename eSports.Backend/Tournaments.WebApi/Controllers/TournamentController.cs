﻿using eSports.Domain.Players.Filter;
using eSports.Domain.Players.ViewModels;
using eSports.Domain.Tournament.Filter;
using eSports.Domain.Tournament.ViewModels;
using eSports.Service.Tournaments.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Tournaments.WebApi.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService) =>
            _tournamentService = tournamentService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTournamentViewModel model)
        {
            var response = await _tournamentService.Create(model);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        public async Task<IActionResult> TournamentHandler(TournamentFilter filter)
        {
            var response = await _tournamentService.GetAllTournaments(filter);

            return Json(new { data = response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTournament([FromBody] int id)
        {
            var response = await _tournamentService.Delete(id);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTournamentStage(TournamentViewModel model)
        {
            var response = await _tournamentService.UpdateTournament(model);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        [Route("Tournament/GetTournament/{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var response = await _tournamentService.GetTournament(id);

            return Json(new { data = response.Data });
        }
    }
}
