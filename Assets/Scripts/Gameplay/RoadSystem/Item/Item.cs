using System;
using ObjectPooling;
using UnityEngine;

namespace Gameplay.RoadSystem.Item
{
    [RequireComponent(typeof(Collider))]
    public class Item : MonoBehaviour, IPoolable
    {
        [SerializeField] private int _amount;

        public int Amount => _amount;
        public Transform Transform => this.transform;
        public GameObject GameObject => this.gameObject;
        
        public event Action<IPoolable> OnReturnToPool;
        public void Reset() => 
            OnReturnToPool?.Invoke(this);
    }
}