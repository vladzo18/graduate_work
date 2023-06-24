using System;
using UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.SettingsScreen
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private ViewType _viewType;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private UIButtonView _backButton;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _backgroundMusicSlider;
        
        public event Action<float> OnSoundVolumeChanged; 
        public event Action<float> OnBackgroundVolumeChanged; 

        public UIButtonView BackButton => _backButton;

        private void Start()
        {
            _soundSlider.onValueChanged.AddListener((v => OnSoundVolumeChanged?.Invoke(v)));
            _backgroundMusicSlider.onValueChanged.AddListener((v => OnBackgroundVolumeChanged?.Invoke(v)));
        }

        private void OnDestroy()
        {
            _soundSlider.onValueChanged.RemoveAllListeners();
            _backgroundMusicSlider.onValueChanged.RemoveAllListeners();
        }

        public void SetVisibility(bool status) => 
            _canvas.enabled = status;

        public void SetSoundSliderValue(float value) => 
            _soundSlider.value = value;

        public void SetMusicSliderValue(float value) => 
            _backgroundMusicSlider.value = value;
    }
}