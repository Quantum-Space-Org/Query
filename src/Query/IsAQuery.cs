namespace Quantum.Query;

public class IsAQuery : IAmAQuery
{
    public string Id { get; set; } = "query-" + Guid.NewGuid().ToString();
    public string CorrelationId { get; set; }
    public DateTime RequestedOn { get; set; } = DateTime.UtcNow;
}