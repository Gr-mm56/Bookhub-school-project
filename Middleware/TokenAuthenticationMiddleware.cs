using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Middleware;

public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TokenAuthenticationMiddleware> _logger;
    private const string HardcodedToken = "YourHardcodedToken";

    public TokenAuthenticationMiddleware(RequestDelegate next, ILogger<TokenAuthenticationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        string token = context.Request.Headers.Authorization;

        if (string.IsNullOrEmpty(token))
        {
            _logger.LogWarning("Unauthorized: Token is missing");
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { message = "Unauthorized user - token is missing" });
            return;
        }

        if (!token.Contains(HardcodedToken))
        {
            _logger.LogWarning("Unsuccessful authorization - invalid token");
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(new { message = "Forbidden - invalid token" });
            return;
        }

        await _next(context);
    }
}