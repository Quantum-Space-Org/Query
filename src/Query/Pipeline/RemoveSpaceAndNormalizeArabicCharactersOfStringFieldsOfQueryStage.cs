using Quantum.Core;

namespace Quantum.Query.Pipeline;

public class RemoveSpaceAndNormalizeArabicCharactersOfStringFieldsOfQueryStage : IAmQueryHandlerStage
{
    public override async Task<TResult> Handle<TQuery, TResult>(TQuery query)
    {
        query.NormalizeArabicCharacters();

        if (Next is not null)
            return await Next.Handle<TQuery, TResult>(query);

        return default;
    }
}