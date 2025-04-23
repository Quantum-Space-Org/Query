namespace Quantum.Query.Exceptions
{
    public class QueryHandlerNotRegisteredException<TQuery, TResult> : Exception where TQuery : IAmAQuery
    {
        public Type QueryHandlerType { get; }

        public QueryHandlerNotRegisteredException(Type queryHandlerType)
        :base($"{queryHandlerType} is not registered!")
            => QueryHandlerType = queryHandlerType;
    }
}