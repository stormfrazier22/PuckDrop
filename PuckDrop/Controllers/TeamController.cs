using Microsoft.AspNetCore.Mvc;
using PuckDrop.Application.Interfaces;

namespace PuckDrop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IAppRepository _appRepository;

        public class TeamName
        {
            [System.ComponentModel.DataAnnotations.Required]
            public string Name { get; set; }
        }

        public TeamController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }


        [HttpGet("getTeamId")]
        public async Task<IActionResult> GetTeamId(TeamName team)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Please enter a valid team name");
                }
                var teamId = await _appRepository.GetTeamId(team.Name);

                return Ok($"Team id for the {team.Name} is : {teamId}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while sending game info.");
            }
        }





    }


}
