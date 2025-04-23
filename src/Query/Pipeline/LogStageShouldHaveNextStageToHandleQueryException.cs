namespace Quantum.Query.Pipeline;

public class LogStageShouldHaveNextStageToHandleQueryException<T, T1> : Exception
{
    public IAmAQuery Query { get; }

    public LogStageShouldHaveNextStageToHandleQueryException(IAmAQuery query)
    {
        Query = query;
    }
}