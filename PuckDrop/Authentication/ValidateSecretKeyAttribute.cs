using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PuckDrop.Authentication
{
    public class ValidateSecretKeyAttribute : ActionFilterAttribute
    {
        private readonly IConfiguration _config;

        public ValidateSecretKeyAttribute(IConfiguration configuration)
        {
            _config = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("SecretKey", out var secretKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var expectedSecretKey = _config["SecretKey"];

            if (secretKey != expectedSecretKey)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }

}
