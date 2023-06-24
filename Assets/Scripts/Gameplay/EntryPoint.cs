using AudioSystem;
using Gameplay.HudSystem;
using Gameplay.RoadSystem;
using Gameplay.RoadSystem.EnemyCarSubsystem;
using Gameplay.RoadSystem.Item;
using Gameplay.RoadSystem.ObstacleSubsystem;
using Gameplay.TutorialSystem;
using Gameplay.VehicleSystem;
using GameState;
using InputSystem;
using ServiceLocator;
using UIElements;
using UnityEngine;

namespace Gameplay
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform _startPointTransform;
        [SerializeField] private HUD _hudView;
        [SerializeField] private VehicleItemsStorage _vehicleStorage;
        [SerializeField] private MapsGameStorage _mapsGameStorage;
        
        [Header("Vehicle Consumers")]
        [SerializeField] private EnemyCarSpawner _enemyCarSpawner;
        [SerializeField] private ItemSpawner _itemSpawner;
        [SerializeField] private ObstacleSpawner _obstacleSpawner;
        [SerializeField] private RoadGenerator _roadGenerator;
        
        [Header("Vehicle Dependency")]
        [SerializeField] private TargetObjectCameraFollower _cameraFollower;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private InputService _inputService;
        [SerializeField] private DeathOverlay _deathOverlay;
        
        private IGameState _gameState;
        private IAudioSystem _audioSystem;
        private HUDController _hudController;
        private VehicleEntity _vehicleEntity;

        private void Start()
        {
            _gameState = Locator.Inctance.GetService<IGameState>();
            _audioSystem = Locator.Inctance.GetService<IAudioSystem>();
            
            VehicleItemDescriptor descriptor = GetVehicleItemDescriptor();
            _vehicleEntity = Instantiate(descriptor.VehicleEntity, _startPointTransform).GetComponent<VehicleEntity>();
            _vehicleEntity.Initialize(_inputService, _cameraFollower, _deathOverlay);
            InitializeVehicleConsumers();
            
            _enemyCarSpawner.SetCars(GetMapStorageDescriptor().EnemyCars);
            _itemSpawner.SetPrefab(descriptor.ItemPrefab.GetComponent<Item>());
            
            _audioSystem.PlayBackground(GetMapStorageDescriptor()._mapMusic);
            
            TryMakeTutorial();
        }

        private void OnDestroy()
        {
            _hudController.Dispose();
            
            _audioSystem.StopBackground();
        }


        private VehicleItemDescriptor GetVehicleItemDescriptor()
        {
            foreach (var descriptor in _vehicleStorage.VehicleItemDescriptors)
                if (descriptor.Type == _gameState.UserStateData.CurrentVehicleType)
                    return descriptor;
            
            return null;
        }

        private MapsGameStorageDescriptor GetMapStorageDescriptor()
        {
            for (int i = 0; i < _mapsGameStorage.MapsGameStorageDescriptors.Count; i++)
                if (_mapsGameStorage.MapsGameStorageDescriptors[i].MapType == _gameState.UserStateData.CurrentMapType)
                    return _mapsGameStorage.MapsGameStorageDescriptors[i];

            return null;
        }

        private void InitializeVehicleConsumers()
        {
            _enemyCarSpawner.Initialize(_vehicleEntity.transform);
            _itemSpawner.Initialize(_vehicleEntity.transform);
            _obstacleSpawner.Initialize(_vehicleEntity.transform);
            _roadGenerator.Initialize(_vehicleEntity.transform, GetMapStorageDescriptor().MapType);
            _cameraController.Initialize(_vehicleEntity.transform, _vehicleEntity.GetComponent<Rigidbody>());
            _hudController = new HUDController(_hudView, _vehicleEntity);
        }

        private void TryMakeTutorial()
        {
            if (!_gameState.UserStateData.IsPlayed)
            {
                Tutorial tutorial = new Tutorial(_vehicleEntity.GetComponent<VehicleMover>(), _enemyCarSpawner, _itemSpawner, _inputService.SwipeDetector, _vehicleEntity.ItemsCollector, _obstacleSpawner);
                tutorial.StartTutorial();
                _gameState.UserStateData.IsPlayed = true;
            }
            else
            {
                _enemyCarSpawner.StartSpawn();
                _itemSpawner.StartSpawn();
                _obstacleSpawner.StartSpawn();
            }
        }
    }
}