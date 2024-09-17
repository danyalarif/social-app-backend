
using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace social_app_backend.Utils;
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception,
            $"An error occurred while processing your request: {exception.Message}");

        await httpContext
            .Response
            .WriteAsJsonAsync(new { success = false, data = exception.Message }, cancellationToken);

        return true;
    }
}
