using eSports.Domain.Stats.Filter;
using eSports.Domain.Stats.ViewModels;
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
        public async Task<IActionResult> Create(StatsViewModel model)
        {
            var response = await _statsService.Create(model);

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

        [HttpPut]
        public async Task<IActionResult> UpdateStats([FromBody] ResultMatchViewModel model)
        {
            var response = await _statsService.Update(model);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok();
        }
    }
}
