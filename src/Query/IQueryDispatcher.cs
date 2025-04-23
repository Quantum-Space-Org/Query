namespace Quantum.Query;

public interface IQueryDispatcher
{
    Task<TResult> RunQuery<TQuery, TResult>(TQuery query) where TQuery : IAmAQuery;
}