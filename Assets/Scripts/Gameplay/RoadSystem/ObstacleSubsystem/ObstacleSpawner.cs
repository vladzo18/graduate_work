using ObjectPooling;
using UnityEngine;

namespace Gameplay.RoadSystem.ObstacleSubsystem
{
    public class ObstacleSpawner : BaseSpawner<Barrier>
    {
        [SerializeField] private Barrier _barrierPrefab;
        
        protected override void SpawnLogic()
        {
            Vector3 position = new Vector3(this.GetRandomXPosition(true), 0, this.CalculateSpawnZPosition());

            while (!CheckIsFree(position))
                position.z += 2;
            
            Barrier barrier = ObjectPool.Instance.GetObject(_barrierPrefab);
            barrier.transform.position = position;
            barrier.transform.parent = this.transform;
            
            this.AddActiveObject(barrier);
        }

        private bool CheckIsFree(Vector3 position)
        {
            Collider[] colliders = Physics.OverlapBox(position, Vector3.one / 2f, Quaternion.identity, LayerMask.GetMask("EnemyCar", "Item"));
            return colliders.Length == 0;
        }
    }
    
}