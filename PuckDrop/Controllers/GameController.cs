using Microsoft.AspNetCore.Mvc;
using PuckDrop.Authentication;
using PuckDrop.Model;
using PuckDrop.Services;
using System.Threading.Tasks;

namespace PuckDrop.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ValidateSecretKeyAttribute))]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameMonitorService _gameMonitorService;

        public GameController(IGameMonitorService gameMonitorService)
        {
            _gameMonitorService = gameMonitorService;
        }

        [HttpPost("sendGameInfo")]
        public async Task<IActionResult> SendGameInfo()
        {
            try
            {
                await _gameMonitorService.ProcessUserNotifications();
                return Ok("Game schedules checked and notifications sent as needed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while sending game info.");
            }
        }
    }
}
