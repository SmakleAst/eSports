using eSports.Domain.Players.Filter;
using eSports.Domain.Players.ViewModels;
using eSports.Service.Players.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Players.WebApi.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService computerService) =>
            (_playerService) = (computerService);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlayerViewModel model)
        {
            var response = await _playerService.Create(model);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        public async Task<IActionResult> PlayerHandler(PlayerFilter filter)
        {
            var response = await _playerService.GetAllPlayers(filter);

            return Json(new { data = response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePlayer([FromBody] int id)
        {
            var response = await _playerService.Delete(id);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlayer(PlayerViewModel model)
        {
            var response = await _playerService.Update(model);

            if (response.StatusCode == eSports.Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpGet]
        [Route("Player/GetPlayer/{id}")]
        public async Task<IActionResult> GetPlayer(int id)
        {
            var response = await _playerService.GetPlayer(id);

            return Json(new { data = response.Data });
        }
    }
}
