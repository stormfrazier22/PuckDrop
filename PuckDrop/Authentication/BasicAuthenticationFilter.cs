using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using PuckDrop.Application.DbContexts;
using BCrypt.Net;
using PuckDrop.Application.Interfaces;

namespace PuckDrop.Authentication
{
    public class BasicAuthenticationFilter : IAsyncActionFilter
    {
        private readonly IAppRepository _userRepository;

        public BasicAuthenticationFilter(IAppRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Username", out var username)
                || !context.HttpContext.Request.Headers.TryGetValue("Password", out var password))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var user = await _userRepository.GetUserAsync(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password.ToString(), user.HashedPassword))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }

}
