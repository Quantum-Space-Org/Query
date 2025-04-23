namespace Quantum.Query.Semaphore
{
    public interface ISemaphore
    {
        void Enter();
        void Exit();
        bool IsThereAnyoneStill();
    }
}