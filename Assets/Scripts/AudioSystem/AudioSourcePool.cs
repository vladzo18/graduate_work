using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AudioSystem 
{
    public class AudioSourcePool 
    {
        private readonly GameObject _parent;
        private readonly List<AudioSource> _audioFreeSources;
        
        private const int StartSourceCount = 3;

        public List<AudioSource> SourcesInUse { get; }

        public AudioSourcePool(GameObject parent) 
        {
            _audioFreeSources = new List<AudioSource>();
            SourcesInUse = new List<AudioSource>();
            _parent = parent;
            
            for (int i = 0; i < StartSourceCount; i++)
                _audioFreeSources.Add(CreateAudioSource());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public AudioSource GetFreeAudioSource() 
        {
            AudioSource source;
            
            if (_audioFreeSources.Count > 0) 
            {
                source = _audioFreeSources.Last();
                _audioFreeSources.Remove(source);
            } 
            else 
            {
                source = CreateAudioSource();
            }
            
            SourcesInUse.Add(source);
            return source;
        }

        public void ReturnToPool(AudioSource audioSource) 
        {
            audioSource.clip = null;
            SourcesInUse.Remove(audioSource);
            _audioFreeSources.Add(audioSource);
        }

        private AudioSource CreateAudioSource()
        {
            AudioSource source = _parent.AddComponent<AudioSource>();
            source.playOnAwake = false;
            return source;
        }
    }
}