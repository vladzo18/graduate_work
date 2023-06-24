using System.Collections.Generic;
using Gameplay.EnemyCarSystem;
using ObjectPooling;
using UnityEngine;

namespace Gameplay.RoadSystem.EnemyCarSubsystem
{
    public class EnemyCarSpawner : BaseSpawner<EnemyCar>
    {
        [SerializeField] private List<EnemyCar> _carsToSpawnPrefabs;

        public void SetCars(List<EnemyCar> cars) => _carsToSpawnPrefabs = cars;
        
        protected override void SpawnLogic()
        {  
            Vector3 position = new Vector3(this.GetRandomXPosition(false), 0, this.CalculateSpawnZPosition());
            
            EnemyCar car = ObjectPool.Instance.GetObject(GetRandomCarPrefab());
            car.transform.parent = this.transform;
            
            car.EnemyCarMover.SetMovementAxis(position);
            
            car.transform.rotation = Quaternion.Euler(0, 180, 0);
            
            car.Spawn();
            
            this.AddActiveObject(car);
        }

        private EnemyCar GetRandomCarPrefab() => 
            _carsToSpawnPrefabs[Random.Range(0, _carsToSpawnPrefabs.Count)];
    }
}