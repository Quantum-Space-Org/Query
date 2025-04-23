namespace Quantum.Query.Pipeline;

public abstract class IAmQueryHandlerStage
{
    public IAmQueryHandlerStage Next;
    public abstract Task<TResult> Handle<TQuery, TResult>(TQuery query) where TQuery : IAmAQuery;
}