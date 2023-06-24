using GameState;
using Menu.MapsShop;
using UnityEngine;

namespace Gameplay.RoadSystem
{
    public class RoadView : MonoBehaviour
    {
        [SerializeField] private MapsGameStorage _mapsGameStorage;
        [SerializeField] private MeshRenderer _roadMeshRenderer;
        [SerializeField] private MeshRenderer _enviromentMeshRenderer;

        public void Set(MapType mapType)
        {
            foreach (var descriptor in _mapsGameStorage.MapsGameStorageDescriptors)
            {
                if (descriptor.MapType == mapType)
                {
                    _roadMeshRenderer.material = descriptor.RoadMaterial;
                    _enviromentMeshRenderer.material = descriptor.EnviromentMaterial;
                    GameObject go = Instantiate(descriptor.EnviromentLayerPrefab, this.transform.position, Quaternion.identity);
                    go.transform.parent = this.transform;
                }
            }
        }
    }
}