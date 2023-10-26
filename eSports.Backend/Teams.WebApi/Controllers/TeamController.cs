using eSports.Domain.Teams.Filter;
using eSports.Domain.Teams.ViewModels;
using eSports.Service.Teams.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Teams.WebApi.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService) =>
            _teamService = teamService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeamViewModel model)
        {
            var response = await _teamService.Create(model);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        public async Task<IActionResult> TeamHandler(TeamFilter filter)
        {
            var response = await _teamService.GetAllTeams(filter);

            return Json(new { data = response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTeam(TeamViewModel model)
        {
            var response = await _teamService.Delete(model);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeam(TeamViewModel model)
        {
            var response = await _teamService.Update(model);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }
    }
}
