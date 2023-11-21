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

        [HttpPut]
        public async Task<IActionResult> SimulateStage([FromBody] int id)
        {
            var response = await _tournamentService.SimulateTournamentStage(id);

            return Json(new { data = response.Data });
        }

        [HttpGet]
        [Route("Tournament/GetTournament/{id}")]
        public async Task<IActionResult> GetTournament(int id)
        {
            var response = await _tournamentService.GetTournament(id);

            return Json(new { data = response.Data });
        }

        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok();
        }
    }
}
