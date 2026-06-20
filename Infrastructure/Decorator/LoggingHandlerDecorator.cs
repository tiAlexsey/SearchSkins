using System.Diagnostics;
using Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Decorator;

public class LoggingHandlerDecorator<TRequest, TResponse>(
    IHandler<TRequest, TResponse> inner,
    ILogger<LoggingHandlerDecorator<TRequest, TResponse>> logger) : IHandler<TRequest, TResponse>
{
    public async Task<TResponse> HandleAsync(TRequest request, CancellationToken ct = default)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName}", requestName);

        var stopwatch = Stopwatch.StartNew();

        var response = await inner.HandleAsync(request, ct);

        stopwatch.Stop();

        logger.LogInformation("Handled {RequestName} in {Elapsed}ms", requestName, stopwatch.ElapsedMilliseconds);

        return response;
    }
}