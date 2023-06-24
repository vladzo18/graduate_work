using ServiceLocator;

namespace Gameplay.PauseSystem
{
    public interface IPauseSystem : IPausable, IService
    {
        bool IsPaused { get; }
        void RegisterPausable(IPausable pausable);
        void UnRegisterPausable(IPausable pausable);
    }
}