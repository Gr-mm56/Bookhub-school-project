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

        if (!token.Contains(HardcodedToken))
        {
            _logger.LogWarning("Unsuccessful authorization");
            context.Response.StatusCode = 403;
        }
        else
        {
            await _next(context);
        }
    }
}