using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PuckDrop.Application.DbContexts;
using PuckDrop.Application.Interfaces;
using PuckDrop.Core.Models;
using PuckDrop.Core.Objects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PuckDrop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppRepository _appRepository;

        public UserController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var teamExists = await _appRepository.TeamExistsAsync(model.TeamId);

                if (!teamExists)
                {
                    return BadRequest("Provided TeamId does not exist.");
                }

                var userExists = await _appRepository.UserExistsAsync(model.Username);

                if (userExists)
                {
                    return BadRequest("Username already exists.");
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                User newUser = new User
                {
                    Username = model.Username,
                    HashedPassword = hashedPassword,
                    PhoneNumber = model.PhoneNumber,
                    TeamId = model.TeamId
                };

                _appRepository.AddUser(newUser);

                await _appRepository.SaveChangesAsync();

                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteUser")]
        public async Task<ActionResult> Deativate(UserDeactivationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _appRepository.GetUserAsync(model.Username);

                if(user == null)
                {
                    return BadRequest("User does not exist");
                }
                if (!BCrypt.Net.BCrypt.Verify(model.Password, user.HashedPassword))
                {
                    return BadRequest("Incorrect password");
                }
                _appRepository.DeleteUser(user);

                await _appRepository.SaveChangesAsync();

                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
