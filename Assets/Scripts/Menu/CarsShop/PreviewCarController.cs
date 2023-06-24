using Gameplay;
using GameState;
using ServiceLocator;
using UIElements;

namespace Menu.CarsShop
{
    public class VehicleShopController
    {
        private readonly VehicleShopView _vehicleShopView;
        private readonly VehicleShopStorage _vehicleShopStorage;
        private readonly IGameState _gameState;

        private int _index;

        public VehicleShopController(VehicleShopView vehicleShopView)
        {
            _vehicleShopView = vehicleShopView;
            _vehicleShopStorage = _vehicleShopView.VehicleShopStorage;
            _gameState = Locator.Inctance.GetService<IGameState>();
            
            _vehicleShopView.NextButton.OnButtonClicked += OnNextButtonClickedHandler;
            _vehicleShopView.PreviousButton.OnButtonClicked += OnPreviousButtonClickedHandler;
            _vehicleShopView.StoreActionButton.OnButtonClick += OnStoreActionButtonClickedHandler;
        }

        public void Initialize()
        {
            _vehicleShopView.SetCoinAmount(_gameState.UserStateData.PointsAmount);
            _index = GetCurrentIndex();
            SwitchVehicle();
        }

        public void Dispose()
        {
            _vehicleShopView.NextButton.OnButtonClicked -= OnNextButtonClickedHandler;
            _vehicleShopView.PreviousButton.OnButtonClicked -= OnPreviousButtonClickedHandler;
            _vehicleShopView.StoreActionButton.OnButtonClick -= OnStoreActionButtonClickedHandler;
        }

        private void OnStoreActionButtonClickedHandler(StoreActionButton.StoreActionButtonState state)
        {
            switch (state)
            {
                case StoreActionButton.StoreActionButtonState.Use:
                    _gameState.UserStateData.CurrentVehicleType = _vehicleShopView.VehicleShopStorage.VehicleDescriptors[_index].Type;
                    _vehicleShopView.StoreActionButton.SetInter(false);
                    _vehicleShopView.StoreActionButton.MakeUseSound();
                    break;
                case StoreActionButton.StoreActionButtonState.Buy:
                    if (_gameState.UserStateData.PointsAmount >= _vehicleShopView.VehicleShopStorage.VehicleDescriptors[_index].Price)
                    {
                        _gameState.UserStateData.PointsAmount -= _vehicleShopView.VehicleShopStorage.VehicleDescriptors[_index].Price;
                        _gameState.UserStateData.AvalibleVehicleTypes.Add(_vehicleShopView.VehicleShopStorage.VehicleDescriptors[_index].Type);
                        
                        _vehicleShopView.SetCoinAmount(_gameState.UserStateData.PointsAmount);
                        _vehicleShopView.PriceBox(false, 0);
                        _vehicleShopView.SetLock(false);
                        _vehicleShopView.StoreActionButton.SetState(StoreActionButton.StoreActionButtonState.Use);
                        _vehicleShopView.StoreActionButton.MakeBuySound();
                    }
                    else
                    {
                        _vehicleShopView.StoreActionButton.MakeBlockedSound();
                    }
                    break;
            }
        }

        private void OnPreviousButtonClickedHandler()
        {
            _index--;
            
            if (_index <= -1)
                _index = _vehicleShopView.VehicleShopStorage.VehicleDescriptors.Count - 1;
            
            SwitchVehicle();
        }

        private void OnNextButtonClickedHandler()
        {
            _index++;
            
            if (_index >= _vehicleShopView.VehicleShopStorage.VehicleDescriptors.Count)
                _index = 0;
            
            SwitchVehicle();
        }

        private void SwitchVehicle()
        {
            bool isAvaliable = CheckAvailability(_vehicleShopView.VehicleShopStorage.VehicleDescriptors[_index].Type);
            
            _vehicleShopView.SetVehicleIcon(_vehicleShopView.VehicleShopStorage.VehicleDescriptors[_index].Icon);
            _vehicleShopView.SetLock(!isAvaliable);
            _vehicleShopView.SetCarNameText(_vehicleShopView.VehicleShopStorage.VehicleDescriptors[_index].Name);
            _vehicleShopView.VehiclePedestal.Swich(_vehicleShopView.VehicleShopStorage.VehicleDescriptors[_index].Type);
            
            
            if (isAvaliable)
            {
                _vehicleShopView.StoreActionButton.SetState(StoreActionButton.StoreActionButtonState.Use);
                _vehicleShopView.PriceBox(false, 0);
            }
            else
            {
                _vehicleShopView.StoreActionButton.SetState(StoreActionButton.StoreActionButtonState.Buy);
                _vehicleShopView.PriceBox(true, _vehicleShopView.VehicleShopStorage.VehicleDescriptors[_index].Price);
            }
            
            if (_vehicleShopView.VehicleShopStorage.VehicleDescriptors[_index].Type == _gameState.UserStateData.CurrentVehicleType)
            {
                _vehicleShopView.StoreActionButton.SetInter(false);
            }
            else
            {
                _vehicleShopView.StoreActionButton.SetInter(true);
            }
            
        }
        
        private int GetCurrentIndex()
        {
            for (int i = 0; i <  _vehicleShopStorage.VehicleDescriptors.Count; i++)
                if (_vehicleShopStorage.VehicleDescriptors[i].Type == _gameState.UserStateData.CurrentVehicleType)
                    return i;
            
            return -1;
        }

        private bool CheckAvailability(VehicleType vehicleType)
        {
            foreach (var v in _gameState.UserStateData.AvalibleVehicleTypes)
            {
                if (v == vehicleType)
                    return true;
            }

            return false;
        }
    }
}