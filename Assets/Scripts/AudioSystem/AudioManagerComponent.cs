using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSystem 
{
    public sealed class AudioManagerComponent : MonoBehaviour 
    {
        private AudioStorage _audioStorage;
        private AudioSourcePool _audioSourcePool;
        private AudioSource _backgroundSource;
        private Coroutine _playingCoroutine;

        private List<AudioSource> _audioSources;
        private float _volume = 1;
        private float _backgroundVolume = 1;

        private void OnDestroy() => 
            StopCoroutine(_playingCoroutine);

        public void Initialize() 
        {
            _audioSourcePool = new AudioSourcePool(this.gameObject);
            _audioStorage = Resources.Load<AudioStorage>("AudioStorage");
            _audioSources = new List<AudioSource>();
            _playingCoroutine = StartCoroutine(PlayingRoutine());
        }

        public void PlayAudio(AudioEnum audio) 
        {
            AudioSource source = _audioSourcePool.GetFreeAudioSource();
            _audioSources.Add(source);
            
            source.clip = _audioStorage.GetAudioClip(audio);
            source.volume = _volume;
            source.Play();
        }

        public void PlayBackground(AudioEnum audio) 
        {
            if (_backgroundSource) return;

            _backgroundSource = _audioSourcePool.GetFreeAudioSource();
            _audioSources.Add(_backgroundSource);
            
            _backgroundSource.clip = _audioStorage.GetAudioClip(audio);
            _backgroundSource.loop = true;
            _backgroundSource.volume = _backgroundVolume;
            _backgroundSource.Play();
        }

        public void StopBackground() 
        {
            if (!_backgroundSource) return;
            
            _backgroundSource.Stop();
            _backgroundSource.loop = false;
            _audioSourcePool.ReturnToPool(_backgroundSource);
            _backgroundSource = null;
        }

        public void SetAudioVolume(float volume)
        {
            for (int i = 0; i < _audioSources.Count; i++)
            {
                if (_audioSources[i].loop) continue;
                _audioSources[i].volume = volume;
            }

            _volume = volume;
        }

        public void SetBackgroundVolume(float volume)
        {
            if (!_backgroundSource) return;
            _backgroundSource.volume = volume;
            _backgroundVolume = volume;
        }

        private IEnumerator PlayingRoutine() 
        {
            WaitUntil anyPlaying = new WaitUntil((() => _audioSourcePool.SourcesInUse.Count > 0));

            while (true) 
            {
                yield return anyPlaying;
                CheckIfAudioPlaying();
                yield return null;
            }
        }

        private void CheckIfAudioPlaying() 
        {
            for (int i = 0; i < _audioSourcePool.SourcesInUse.Count; i++) 
            {
                if (_audioSourcePool.SourcesInUse[i].loop) continue;
                if (_audioSourcePool.SourcesInUse[i].isPlaying) continue;
                
                _audioSourcePool.ReturnToPool(_audioSourcePool.SourcesInUse[i]);
            }
        }
    }
}