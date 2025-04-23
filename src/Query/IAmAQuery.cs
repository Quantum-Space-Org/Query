namespace Quantum.Query;

public interface IAmAQuery
{
    string Id { get; set; }
    string CorrelationId { get; set; }
    DateTime RequestedOn { get; set; }
}