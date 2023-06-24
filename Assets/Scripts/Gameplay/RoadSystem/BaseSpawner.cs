using System.Collections;
using System.Collections.Generic;
using Additional;
using Gameplay.Movement;
using Gameplay.PauseSystem;
using ObjectPooling;
using ServiceLocator;
using UnityEngine;

namespace Gameplay.RoadSystem
{
    public abstract class BaseSpawner<SpawnType> : MonoBehaviour, IPausable where SpawnType : IPoolable
    {
        [SerializeField] private float _spawnCooldown;
        [SerializeField] private float _despawnCooldown;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _spawnToPlayerDistance;
        
        private Coroutine _spawnRoutine;
        private Coroutine _despawnRoutine;
        private bool _isStoped;
        
        private List<SpawnType> _activeItems = new List<SpawnType>();
        private float[] _spawnCoordinates = MovementAxis.GetArrayRepresentationOfDefault();
        private float _lastSpawnCoordinate;
        
        private void Start()
        {
            StopSpawn();
            
            _spawnRoutine = CoroutineStarter.Start(SpawnRoutine());
            _despawnRoutine = CoroutineStarter.Start(DespawnRoutine());
            
            ObjectPool.Instance.OnDisposePool += ClearActiveObject;
            Locator.Inctance.GetService<IPauseSystem>().RegisterPausable(this);
        }
        
        private void OnDestroy()
        {
            if (_spawnRoutine != null)
                CoroutineStarter.Stop(_spawnRoutine);
            if (_despawnRoutine != null)
                CoroutineStarter.Stop(_despawnRoutine);
            
            ObjectPool.Instance.OnDisposePool -= ClearActiveObject;
            Locator.Inctance.GetService<IPauseSystem>().UnRegisterPausable(this);
        }

        public void Initialize(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            //StartSpawn();
        }

        public void StopSpawn() => 
            _isStoped = true;

        public void StartSpawn() => 
            _isStoped = false;

        public void SetPaused(bool status)
        {
            if (status)
            {
                StopSpawn();
                return;
            }
            
            StartSpawn();
        }

        private IEnumerator SpawnRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(_spawnCooldown);
            WaitWhile waitWhile = new WaitWhile(() => _isStoped);
            
            while (true)
            {
                yield return waitWhile;
                SpawnLogic();
                yield return wait;
            }
        }

        private IEnumerator DespawnRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(_despawnCooldown);
            WaitWhile waitWhile = new WaitWhile(() => _isStoped);
            
            List<SpawnType> itemsToClear = new List<SpawnType>();
            
            while (true)
            {
                yield return waitWhile;

                foreach (var item in _activeItems)
                {
                    if (item.Transform.position.z < _playerTransform.position.z - _spawnToPlayerDistance)
                        itemsToClear.Add(item);
                }

                foreach (var item in itemsToClear)
                {
                    item.Reset();
                    _activeItems.Remove(item);
                }
                
                itemsToClear.Clear();
                
                yield return wait;
            }
        }

        private void ClearActiveObject() => 
            _activeItems.Clear();

        protected abstract void SpawnLogic();

        protected void AddActiveObject(SpawnType spawnObject) => 
            _activeItems.Add(spawnObject);

        protected float CalculateSpawnZPosition() => 
            _playerTransform.position.z + _spawnToPlayerDistance;

        protected float GetPlayerZPosition() =>
            _playerTransform.position.z;

        protected float GetRandomXPosition(bool allowRepetition)
        {
            float current = _spawnCoordinates[Random.Range(0, _spawnCoordinates.Length)];

            if (!allowRepetition)
            {
                while (current == _lastSpawnCoordinate)
                    current = _spawnCoordinates[Random.Range(0, _spawnCoordinates.Length)];
            }
            
            _lastSpawnCoordinate = current;
            
            return current;
        }
    }
}