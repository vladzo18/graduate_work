using Additional;
using Gameplay.PauseSystem;
using Gameplay.VehicleSystem;
using GameState;
using ServiceLocator;

namespace Gameplay.HudSystem
{
    public class HUDController
    {
        private readonly HUD _hudView;
        private readonly VehicleEntity _vehicleEntity;
        private readonly IPauseSystem _pauseSystem;
        private readonly IGameState _gameState;
        
        public HUDController(HUD hudView, VehicleEntity vehicleEntity)
        {
            _hudView = hudView;
            _vehicleEntity = vehicleEntity;
            _pauseSystem = Locator.Inctance.GetService<IPauseSystem>();
            _gameState = Locator.Inctance.GetService<IGameState>();
            
            _hudView.ItemsCollectorView.Initialize(vehicleEntity.ItemsCollector);
            _hudView.MeterCounterView.Initialize(vehicleEntity.MeterCounter);

            _vehicleEntity.VehicleDeath.OnDeath += OnDeathHandler;
            _hudView.DeathWindow.OnBackButtonClicked += OnBackButtonClicked;
            _hudView.PauseWindow.PlayButton.OnButtonClicked += OnPlayButtonClicked;
            _hudView.PauseWindow.BackToMenuButton.OnButtonClicked += OnBackButtonClicked;
            _hudView.PauseButton.OnButtonClicked += OnUiPauseButtonClicked;
        }
        
        public void Dispose()
        {
            _vehicleEntity.VehicleDeath.OnDeath -= OnDeathHandler;
            _hudView.DeathWindow.OnBackButtonClicked -= OnBackButtonClicked;
            _hudView.PauseWindow.PlayButton.OnButtonClicked -= OnPlayButtonClicked;
            _hudView.PauseWindow.BackToMenuButton.OnButtonClicked -= OnBackButtonClicked;
            _hudView.PauseButton.OnButtonClicked -= OnUiPauseButtonClicked;
        }
        
        private void OnDeathHandler()
        {
            _hudView.DeathWindow.SetMetersAmount(_vehicleEntity.MeterCounter.Meters);
            _hudView.DeathWindow.SetPointsAmount(_vehicleEntity.ItemsCollector.Amount);
            _hudView.DeathWindow.Show();
            
            _pauseSystem.SetPaused(true);
            
            _gameState.UserStateData.PointsAmount += _vehicleEntity.ItemsCollector.Amount;
            _gameState.UserStateData.Rating.Add(_vehicleEntity.MeterCounter.Meters);
        }

        private void OnUiPauseButtonClicked()
        {
            _hudView.PauseWindow.Show();
            _pauseSystem.SetPaused(true);
        }

        private void OnPlayButtonClicked()
        {
            _hudView.PauseWindow.Hide();
            _pauseSystem.SetPaused(false);
        }

        private void OnBackButtonClicked()
        {
            //ObjectPooling.ObjectPool.Instance.DisposeTask();
            _pauseSystem.SetPaused(false);
            SceneLoader.LoadScene(SceneLoader.MENU_SCENE_INDEX);
        }
    }
}