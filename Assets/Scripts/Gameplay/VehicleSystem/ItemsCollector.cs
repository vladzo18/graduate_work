using System;
using AudioSystem;
using Gameplay.RoadSystem.Item;
using ServiceLocator;
using UnityEngine;

namespace Gameplay.VehicleSystem
{
    public class ItemsCollector : MonoBehaviour
    {
        private const float AudioResetDelay = 0.1f;
        
        private IAudioSystem _audioSystem = null;
        private bool _isAudioPlaying;
        
        public int Amount { get; private set; }
        
        public Action<int> OnItemsCollected;

        private void Start() => 
            _audioSystem = Locator.Inctance.GetService<IAudioSystem>();

        private void OnTriggerEnter(Collider other)
        {
            Item item = other.GetComponent<Item>();

            if (item)
            {
                OnItemsCollected?.Invoke(item.Amount);
                Amount += item.Amount;
                item.Reset();

                if (!_isAudioPlaying)
                {
                    _audioSystem.PlayAudio(AudioEnum.ItemCollect);
                    _isAudioPlaying = true;
                    Invoke(nameof(ResetAudioPlaying), AudioResetDelay);
                }
            }
        }
        
        private void ResetAudioPlaying() => 
            _isAudioPlaying = false;
    }
}