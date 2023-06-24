using ObjectPooling;
using UnityEngine;

namespace Gameplay.RoadSystem.Item
{
    public class ItemSpawner : BaseSpawner<Item>
    {
        [SerializeField] private Item _itemPrefab;

        public void SetPrefab(Item pregab) => _itemPrefab = pregab;

        protected override void SpawnLogic()
        {
            Vector3 position = new Vector3(this.GetRandomXPosition(true), 1, this.CalculateSpawnZPosition());

            Item item = ObjectPool.Instance.GetObject(_itemPrefab);
            item.transform.parent = this.transform;
            item.transform.position = position;
            
            this.AddActiveObject(item);
        }
    }
}