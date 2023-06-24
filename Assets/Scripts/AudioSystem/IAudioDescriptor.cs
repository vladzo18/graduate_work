using UnityEngine;

namespace AudioSystem
{
    public interface IAudioDescriptor
    {
        public AudioEnum Audio { get; }
        public AudioClip AudioClip { get; }
    }
}