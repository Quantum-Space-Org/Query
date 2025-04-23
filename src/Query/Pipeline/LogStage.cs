using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Quantum.Query.Pipeline;

public class LogStage : IAmQueryHandlerStage
{
    private readonly ILogger<LogStage> _logger;

    public LogStage(ILogger<LogStage> logger)
    {
        _logger = logger;
    }

    public override async Task<TResult> Handle<TQuery, TResult>(TQuery query)
    {
        if (Next is null)
            throw new LogStageShouldHaveNextStageToHandleQueryException<TQuery, TResult>(query);

        var sw = Stopwatch.StartNew();

        var handle = await Next.Handle<TQuery, TResult>(query);

        sw.Stop();

        _logger.LogDebug($"The execution time of handling {query.GetType().Name} is {sw.ElapsedMilliseconds} in ms)");

        return handle;
    }
}