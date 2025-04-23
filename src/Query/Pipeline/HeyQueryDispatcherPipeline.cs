using System.Collections.Generic;

namespace Quantum.Query.Pipeline;

public class HeyQueryDispatcherPipeline
{
    private readonly ICollection<IAmQueryHandlerStage> _stages = new List<IAmQueryHandlerStage>();
    private IAmQueryHandlerStage _lastStage;

    public static HeyQueryDispatcherPipeline CreateAQueryDispatcher()
    {
        return new HeyQueryDispatcherPipeline();
    }

    public HeyQueryDispatcherPipeline WithStartingStage(IAmQueryHandlerStage stage)
    {
        if (_lastStage is not null)
            _lastStage.Next = stage;

        _lastStage = stage;
        _stages.Add(stage);
        return this;
    }

    public HeyQueryDispatcherPipeline ProceedBy(IAmQueryHandlerStage stage)
    {
        if (_lastStage is not null)
            _lastStage.Next = stage;

        _lastStage = stage;
        _stages.Add(stage);
        return this;
    }

    public IQueryDispatcher ThankYou()
    {
        return new PipelineQueryDispatcher(_stages);
    }
}