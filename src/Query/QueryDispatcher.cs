using System.Linq;
using System.Reflection;
using Quantum.Core.Exceptions;
using Quantum.Query.Exceptions;
using Quantum.Resolver;

namespace Quantum.Query;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IResolver _resolver;

    public QueryDispatcher(IResolver resolver) => _resolver = resolver;

    public async Task<TResult> RunQuery<TQuery, TResult>(TQuery query) where TQuery : IAmAQuery
    {
        try
        {

            var queryHandler = _resolver.Resolve<IWantToHandleThisQuery<TQuery, TResult>>();

            try
            {
                return await queryHandler.RunQuery(query);
            }
            catch (Exception e)
            {
                var firstOrDefault = IsHandlerImplementedFallback(queryHandler);
                if (firstOrDefault is not null)
                    return await queryHandler.FallBack(query, e);

                throw;
            }
        }
        catch (QuantumComponentIsNotRegisteredException e)
        {
            throw new QueryHandlerNotRegisteredException<TQuery, TResult>(typeof(TQuery));
        }
    }

    private static MethodInfo IsHandlerImplementedFallback<TQuery, TResult>(IWantToHandleThisQuery<TQuery, TResult> queryHandler)
        where TQuery : IAmAQuery
    {
        return queryHandler.GetType().GetMethods().FirstOrDefault(a => a.Name == nameof(queryHandler.FallBack));
    }
}