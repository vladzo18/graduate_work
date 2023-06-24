using System.Collections.Generic;
using System.Linq;
using GameState;
using UnityEngine;

namespace Gameplay.RoadSystem
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private List<Transform> _roads;
        [SerializeField] private float _offset = 50f;
    
        private float _movingOffset;

        private void Start()
        {
            if (_roads != null && _roads.Count > 0)
                _roads = _roads.OrderBy(r => r.position.z).ToList();

            _movingOffset = _offset;
        }

        private void FixedUpdate()
        {
            if (_playerTransform == null)
                return;
            
            if (_playerTransform.position.z > _movingOffset)
            {
                MoveRoad();
                _movingOffset += _offset;
            }
        }

        public void Initialize(Transform playerTransform, MapType mapType)
        {
            _playerTransform = playerTransform;
            foreach (var t in _roads)
            {
                t.GetComponent<RoadView>().Set(mapType);
            }
        }

        private void MoveRoad()
        {
            Transform movedRoad = _roads.First();
            _roads.Remove(movedRoad);
            float newZ = _roads.Last().position.z + _offset;
            movedRoad.position = new Vector3(movedRoad.position.x, movedRoad.position.y, newZ);
            _roads.Add(movedRoad);
        }
    }
}

