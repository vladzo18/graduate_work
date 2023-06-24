using System;
using UnityEngine;

namespace AudioSystem 
{
    [Serializable]
    public class OneClipAudioDescriptor : IAudioDescriptor
    {
        [SerializeField] private AudioEnum _audio;
        [SerializeField] private AudioClip _audioClip;

        public AudioEnum Audio => _audio;
        public AudioClip AudioClip => _audioClip;
    }

    [Serializable]
    public class MultiplyClipAudioDescriptor : IAudioDescriptor
    {
        [SerializeField] private AudioEnum _audio;
        [SerializeField] private AudioClip[] _audioClips;

        private int _index;

        public AudioEnum Audio => _audio;

        public AudioClip AudioClip
        {
            get
            {
                //if (_index == _audioClips.Length - 1)
                    //_index = 0;
                
                //return _audioClips[_index++];
                return _audioClips[0];
            }
        }
    }
}