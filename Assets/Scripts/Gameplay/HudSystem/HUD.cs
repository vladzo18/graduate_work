using UIElements;
using UnityEngine;
using WindowSystem;

namespace Gameplay.HudSystem
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private UIButtonView _pauseButton;
        [SerializeField] private MeterCounterView _meterCounterView;
        [SerializeField] private ItemCounterView _itemsCollectorView;
        [SerializeField] private PauseWindow _pauseWindow;
        [SerializeField] private DeathWindow _deathWindow;

        public UIButtonView PauseButton => _pauseButton;
        public MeterCounterView MeterCounterView => _meterCounterView;
        public ItemCounterView ItemsCollectorView => _itemsCollectorView;
        public PauseWindow PauseWindow => _pauseWindow;
        public DeathWindow DeathWindow => _deathWindow;
    }
}