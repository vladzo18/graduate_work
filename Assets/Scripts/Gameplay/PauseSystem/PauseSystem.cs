using System.Collections.Generic;

namespace Gameplay.PauseSystem
{
    public class PauseSystem : IPauseSystem
    {
        private readonly List<IPausable> _handlers = new List<IPausable>();
        
        public bool IsPaused { get; private set; }

        public void RegisterPausable(IPausable pausable) => _handlers.Add(pausable);
        
        public void UnRegisterPausable(IPausable pausable) => _handlers.Remove(pausable);
        
        public void SetPaused(bool status)
        {
            IsPaused = status;

            foreach (var pausable in _handlers) 
                pausable.SetPaused(status);
        }
    }
}