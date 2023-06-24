using AudioSystem;
using GameState;
using ServiceLocator;

namespace Menu.SettingsScreen
{
    public class SettingsController
    {
        private readonly SettingsView _settingsView;
        private readonly IGameState _gameStat;
        private readonly IAudioSystem _audioSystem;
        
        public SettingsController(SettingsView settingsView)
        {
            _settingsView = settingsView;
            _gameStat = Locator.Inctance.GetService<IGameState>();
            _audioSystem = Locator.Inctance.GetService<IAudioSystem>();
            
            _settingsView.OnSoundVolumeChanged += OnSoundVolumeChangedHandler;
            _settingsView.OnBackgroundVolumeChanged += OnBackgroundVolumeChangedHandler;
            
            _settingsView.SetSoundSliderValue(_gameStat.SettingsStateData.SoundsVolume);
            _settingsView.SetMusicSliderValue(_gameStat.SettingsStateData.MusicVolume);
        }
        
        public void Dispose()
        {
            _settingsView.OnSoundVolumeChanged -= OnSoundVolumeChangedHandler;
            _settingsView.OnBackgroundVolumeChanged -= OnBackgroundVolumeChangedHandler;
        }

        private void OnSoundVolumeChangedHandler(float volume)
        {
            _audioSystem.SetAudioVolume(volume);
            _gameStat.SettingsStateData.SoundsVolume = volume;
        }

        private void OnBackgroundVolumeChangedHandler(float volume)
        {
            _audioSystem.SetBackgroundVolume(volume);
            _gameStat.SettingsStateData.MusicVolume = volume;
        }
    }
}