using Microsoft.AspNetCore.Http;
using Quantum.CorrelationId;

namespace Quantum.Query.Pipeline;

public class CorrelationIdQueryStage : IAmQueryHandlerStage
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorrelationIdQueryStage(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    public override async Task<TResult> Handle<TQuery, TResult>(TQuery query)
    {
        var correlationId = _httpContextAccessor.GetCorrelationId();
        if (correlationId != null) query.CorrelationId = correlationId;

        if (Next is not null)
            return await Next.Handle<TQuery, TResult>(query);

        return default;
    }
}