using UnityEngine;

namespace AudioSystem 
{
    public class AudioManager : IAudioSystem
    {
        private static readonly AudioManagerComponent _audioManagerComponent;
        
        static AudioManager() 
        {
           var go = new GameObject(nameof(AudioManager));
           _audioManagerComponent = go.AddComponent<AudioManagerComponent>();
           _audioManagerComponent.Initialize();
           GameObject.DontDestroyOnLoad(go);
        }
        
        public void PlayAudio(AudioEnum audio) =>
            _audioManagerComponent.PlayAudio(audio);
        
        public void PlayBackground(AudioEnum audio) => 
            _audioManagerComponent.PlayBackground(audio);

        public void StopBackground() => 
            _audioManagerComponent.StopBackground();
        
        public void SetAudioVolume(float volume) => 
            _audioManagerComponent.SetAudioVolume(volume);

        public void SetBackgroundVolume(float volume) => 
            _audioManagerComponent.SetBackgroundVolume(volume);
    } 
}