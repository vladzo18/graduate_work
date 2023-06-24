using System;
using Gameplay.EnemyCarSystem;
using ObjectPooling;
using UnityEngine;

namespace Gameplay.RoadSystem.ObstacleSubsystem
{
    public class Barrier : MonoBehaviour, IPoolable, IEnemy
    {
        public Transform Transform => this.transform;
        public GameObject GameObject => this.gameObject;
        
        public event Action<IPoolable> OnReturnToPool;
        public void Reset() => 
            OnReturnToPool?.Invoke(this);
    }
}