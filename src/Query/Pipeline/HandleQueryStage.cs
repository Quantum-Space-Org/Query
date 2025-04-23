using System.Linq;
using Quantum.Core.Exceptions;
using Quantum.Query.Exceptions;
using Quantum.Resolver;

namespace Quantum.Query.Pipeline;

public class HandleQueryStage : IAmQueryHandlerStage
{
    private readonly IResolver _resolver;

    public HandleQueryStage(IResolver resolver)
        => _resolver = resolver;

    public override async Task<TResult> Handle<TQuery, TResult>(TQuery query)
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
                var firstOrDefault = queryHandler.GetType().GetMethods().FirstOrDefault(a => a.Name == nameof(queryHandler.FallBack));
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
}