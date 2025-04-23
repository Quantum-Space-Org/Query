namespace Quantum.Query;

public interface IWantToHandleThisQuery<TQuery, TResult>
    where TQuery : IAmAQuery
{
    Task<TResult> RunQuery(TQuery query);

    virtual Task<TResult> FallBack(TQuery query, Exception exception)
        => throw new FallBackMethodShouldBeImplementedByQueryHandlerException<TQuery, TResult>(query);
}