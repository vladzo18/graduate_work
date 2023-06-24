using System;
using System.Collections;
using ObjectPooling;
using UnityEngine;

namespace Gameplay.EnemyCarSystem
{
    public class EnemyCar : MonoBehaviour, IPoolable, IEnemy
    {
        [SerializeField] private EnemyCarType _enemyCarType;
        [SerializeField] private EnemyCarMover _enemyCarMover;
        
        public EnemyCarType EnemyCarType => _enemyCarType;
        public EnemyCarMover EnemyCarMover => _enemyCarMover;

        public void Spawn()
        {
            StartCoroutine(SpawnEffect());
            _enemyCarMover.StartMovement();
        }

        private IEnumerator SpawnEffect()
        {
            float duration = 0.25f;
            float journey = 0f;

            float target = this.transform.localScale.x;
            
            while (journey <= duration)
            {
                journey += Time.deltaTime;

                float newXYZ = Mathf.Lerp(0, target, journey / duration);

                this.transform.localScale = new Vector3(newXYZ, newXYZ, newXYZ);

                yield return null;
            }
        }
        
        #region IPoolable
        
        public Transform Transform => this.transform;
        public GameObject GameObject => this.gameObject;
        
        public event Action<IPoolable> OnReturnToPool;
        public void Reset() => 
            OnReturnToPool?.Invoke(this);
        
        #endregion
    }
}