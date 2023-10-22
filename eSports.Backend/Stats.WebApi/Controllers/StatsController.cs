﻿using eSports.Domain.Stats.Filter;
using eSports.Domain.Teams.ViewModels;
using eSports.Service.Stats.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Stats.WebApi.Controllers
{
    public class StatsController : Controller
    {
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService) =>
            _statsService = statsService;

        [HttpPost]
        public async Task<IActionResult> Create(TeamViewModel firstTeam, TeamViewModel secondTeam)
        {
            var response = await _statsService.Create(firstTeam, secondTeam);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        public async Task<IActionResult> StatsHandler(StatsFilter filter)
        {
            var response = await _statsService.GetAllStats(filter);

            return Json(new { data = response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStats(StatsViewModel model)
        {
            var response = await _statsService.Delete(model);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStats(TeamViewModel firstTeam,
            TeamViewModel secondTeam, bool isFirstTeamWin)
        {
            var response = await _statsService.Update(firstTeam, secondTeam, isFirstTeamWin);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }
    }
}