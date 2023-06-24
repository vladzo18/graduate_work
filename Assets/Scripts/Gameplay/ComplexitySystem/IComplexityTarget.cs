using System;

namespace Gameplay.ComplexitySystem
{
    public interface IComplexityTarget
    {
        event Action<int> OnTargetChange;
    }
}