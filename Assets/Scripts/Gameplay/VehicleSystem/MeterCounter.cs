using System;
using Gameplay.ComplexitySystem;
using UnityEngine;

namespace Gameplay.VehicleSystem
{
    public class MeterCounter : MonoBehaviour, IComplexityTarget
    {
        public int Meters { get; private set; }
        
        public event Action OnMeterUp;
        public event Action<int> OnTargetChange;

        private Vector3 _position;

        private void Start()
        {
            _position = this.transform.position;
            Complexity.Instance.SetComplexityTarget(this);
        }

        private void FixedUpdate()
        {
            if (this.transform.position.z >= _position.z + 2)
            {
                _position = this.transform.position;
                Meters++;
                OnMeterUp?.Invoke();
                OnTargetChange?.Invoke(Meters);
            }
        }
    }
}