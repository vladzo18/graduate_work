using InputSystem;
using UIElements;
using UnityEngine;

namespace Gameplay.VehicleSystem
{
    public class VehicleEntity : MonoBehaviour
    {
        [SerializeField] private Death _vehicleDeath;
        [SerializeField] private MeterCounter _meterCounter;
        [SerializeField] private ItemsCollector _itemsCollector;
        [SerializeField] private VehicleLeader _vehicleLeader;

        public Death VehicleDeath => _vehicleDeath;
        public MeterCounter MeterCounter => _meterCounter;
        public ItemsCollector ItemsCollector => _itemsCollector;

        public void Initialize(InputService inputService, TargetObjectCameraFollower camera, DeathOverlay deathOverlay)
        {
            _vehicleLeader.Initialize(inputService, camera, deathOverlay);
        }
    }
}