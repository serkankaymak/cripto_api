
using Domain.Domains.IdentityDomain.JwtService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Validators;


public class JwtValidator : ActionFilterAttribute
{

    JwtTokenService JwtTokenService;

    public JwtValidator(JwtTokenService jwtTokenService)
    {
        JwtTokenService = jwtTokenService;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Authorization başlığını kontrol et
        var hasAuthHeader = context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader);

        // Eğer Authorization başlığı yoksa, 401 Unauthorized döndür
        if (!hasAuthHeader || string.IsNullOrEmpty(authHeader))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Burada özel doğrulama mantığınızı ekleyebilirsiniz.
        // Örneğin, token yapısını kontrol edebilir veya belirli bir değeri doğrulayabilirsiniz.

        var token = authHeader.ToString().Replace("Bearer ", "");

        if (!JwtTokenService.ValidateToken(token)) context.Result = new UnauthorizedResult();

        base.OnActionExecuting(context);
    }
}