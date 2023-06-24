using System;
using AudioSystem;
using Gameplay.EnemyCarSystem;
using ServiceLocator;
using UnityEngine;

namespace Gameplay.VehicleSystem
{
    public class Death : MonoBehaviour
    {
        public event Action OnDeath;

        private IAudioSystem _audioSystem = null;

        private void Start() => 
            _audioSystem = Locator.Inctance.GetService<IAudioSystem>();

        private void OnCollisionEnter(Collision collision)
        {
            var something = collision.transform.GetComponent<IEnemy>();

            if (something != null)
                MakeDeath();
        }
        
        public void MakeDeath()
        {
            _audioSystem.PlayAudio(AudioEnum.DeathSound);
            OnDeath?.Invoke();
        }
    }
}