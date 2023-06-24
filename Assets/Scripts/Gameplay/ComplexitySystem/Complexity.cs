using System.Collections.Generic;

namespace Gameplay.ComplexitySystem
{
    public class Complexity
    {
        private static readonly Complexity _instance = new Complexity();

        public static Complexity Instance => _instance;

        private List<IComplexityReactor> _complexityReactors = new List<IComplexityReactor>();
        private IComplexityTarget _complexityTargets;

        private int _value;

        public void SetComplexityTarget(IComplexityTarget complexityTarget)
        {
            _complexityTargets = complexityTarget;
            _complexityTargets.OnTargetChange += OnTargetChange;
        }
        
        public void SetComplexityReactor(IComplexityReactor complexityReactor)
        {
            _complexityReactors.Add(complexityReactor);
        }
        
        private void OnTargetChange(int value)
        {
            if (value % 50 == 0)
            {
                //IncreaseTarget();
            }
        }
        
        private void IncreaseTarget()
        {
            foreach (var reactor in _complexityReactors)
            {
                reactor.ReactorOnComplexityChange(++_value);
            }
        }
    }
}