using System.Collections.Generic;
using System.Linq;

namespace Quantum.Query.Pipeline;

public class PipelineQueryDispatcher : IQueryDispatcher
{
    private readonly ICollection<IAmQueryHandlerStage> _stages;

    public PipelineQueryDispatcher(ICollection<IAmQueryHandlerStage> stages)
    {
        GuardAgainstEmptyOrNullStagesList(stages);
        _stages = stages;
    }

    public async Task<TResult> RunQuery<TQuery, TResult>(TQuery query) where TQuery : IAmAQuery
    {

        return await StartingStage().Handle<TQuery, TResult>(query);
    }

    private IAmQueryHandlerStage StartingStage() => _stages.FirstOrDefault();

    private void GuardAgainstEmptyOrNullStagesList(ICollection<IAmQueryHandlerStage> stages)
    {
        if (stages == null || !stages.Any())
            throw new PipelineQueryDispatcherStagesIsEmptyOrNullException();
    }
}

public class PipelineQueryDispatcherStagesIsEmptyOrNullException : Exception
{
}