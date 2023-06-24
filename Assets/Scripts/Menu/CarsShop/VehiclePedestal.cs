using System.Collections.Generic;
using Gameplay;
using GameState;
using UnityEngine;

namespace Menu.CarsShop
{
    public class VehiclePedestal : MonoBehaviour
    {
        [SerializeField] private Transform _contentTransform;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private VehicleShopStorage vehicleShopStorage;

        private Dictionary<VehicleType, GameObject> _gameObjects = new Dictionary<VehicleType, GameObject>();
        private GameObject _active = null;
        private bool _isRotating;
        
        private void Awake()
        {
            foreach (var descriptor in vehicleShopStorage.VehicleDescriptors)
            {
                GameObject go = Instantiate(descriptor.ThreeDModel, _contentTransform.position, Quaternion.identity);
                go.transform.parent = _contentTransform;
                go.transform.localScale = Vector3.one * 1.3f;
                _gameObjects.Add(descriptor.Type, go);
                _active = go;
                go.SetActive(false);
            }
        }

        private void Update()
        {
            if(_isRotating) 
                return;
            
            transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
        }

        public void Swich(VehicleType vehicleType)
        { 
            _active?.SetActive(false);
            _active = _gameObjects[vehicleType];
            _active.SetActive(true);
        }

        public void StartRotating() => 
            _isRotating = true;

        public void StopRotating() =>
            _isRotating = false;
    }
    
}
