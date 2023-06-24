using System.Collections.Generic;
using UnityEngine;

namespace AudioSystem {
    
    [CreateAssetMenu(fileName = "AudioStorage", menuName = "AudioStorage", order = 0)]
    public class AudioStorage : ScriptableObject 
    {
        [SerializeField] private List<OneClipAudioDescriptor> _o_audioDescriptors;
        [SerializeField] private List<MultiplyClipAudioDescriptor> _m_audioDescriptors;
        
        public AudioClip GetAudioClip(AudioEnum audioEnum)
         {
             AudioClip ac = _o_audioDescriptors.Find(e => e.Audio == audioEnum)?.AudioClip;

             if (ac == null)
             {
                 ac = _m_audioDescriptors.Find(e => e.Audio == audioEnum)?.AudioClip;
             }

             return ac;
         }
    } 
}
