using AudioSystem;
using GameState;
using Menu.CarsShop;
using Menu.MainScreen;
using Menu.MapsShop;
using Menu.RatingScreen;
using Menu.SettingsScreen;
using ServiceLocator;
using UnityEngine;

namespace Menu
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Views _views;
        
        private MenuController _menuController;
        private VehicleShopController _vehicleShopController;
        private MapsShopController _mapsShopController;
        private RatingController _ratingController;
        private SettingsController _settingsController;
        private ViewsController _viewsController;
        private IGameState _gameState;
        private IAudioSystem _audioSystem;

        private void Awake()
        {
            _menuController = new MenuController(_views.MenuView);
            _vehicleShopController = new VehicleShopController(_views.VehicleShopView);
            _mapsShopController = new MapsShopController(_views.MapsShopView);
            _ratingController = new RatingController(_views.RatingView);
            _settingsController = new SettingsController(_views.SettingsView);
            _viewsController = new ViewsController(_views);
            
            _gameState = Locator.Inctance.GetService<IGameState>();
            _audioSystem = Locator.Inctance.GetService<IAudioSystem>();
        }

        private void Start()
        { 
            _vehicleShopController.Initialize();
            _ratingController.Initialize();
            _viewsController.Initialize();
            
            _audioSystem.PlayBackground(AudioEnum.MenuMusic);
            _audioSystem.SetAudioVolume(_gameState.SettingsStateData.SoundsVolume);
            _audioSystem.SetBackgroundVolume(_gameState.SettingsStateData.MusicVolume);
        }
        
        private void OnDestroy()
        {
            _menuController.Dispose();
            _vehicleShopController.Dispose();
            _mapsShopController.Dispose();
            _ratingController.Dispose();
            _settingsController.Dispose();
            _viewsController.Dispose();
            _gameState.SaveState();
            _audioSystem.StopBackground();
        }
    }
}