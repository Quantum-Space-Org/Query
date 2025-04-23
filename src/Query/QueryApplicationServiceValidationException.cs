using Quantum.Core;
using Quantum.Domain;

namespace Quantum.Query;

public class QueryApplicationServiceValidationException : DomainValidationException
{
    public QueryApplicationServiceValidationException(ValidationResult validationResult) : base(validationResult)
    {
    }
}