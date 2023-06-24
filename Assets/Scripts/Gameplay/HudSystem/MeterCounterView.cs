using Gameplay.VehicleSystem;
using TMPro;
using UnityEngine;

namespace Gameplay.HudSystem
{
    public class MeterCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private MeterCounter _meterCounter;

        private int _current;
        
        private void OnDestroy() => 
            _meterCounter.OnMeterUp -= OnMeterUpHandler;

        public void Initialize(MeterCounter meterCounter)
        {
            _meterCounter = meterCounter;
            _meterCounter.OnMeterUp += OnMeterUpHandler;
        }
        
        private void OnMeterUpHandler()
        {
            _text.text = $"{++_current} M";
        }
    }
}
