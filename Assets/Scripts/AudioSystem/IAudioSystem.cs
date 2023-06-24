using ServiceLocator;

namespace AudioSystem
{
    public interface IAudioSystem : IService
    {
        void PlayAudio(AudioEnum audio);
        void PlayBackground(AudioEnum  audio);
        void StopBackground();
        void SetAudioVolume(float volume);
        void SetBackgroundVolume(float volume);
    }
}