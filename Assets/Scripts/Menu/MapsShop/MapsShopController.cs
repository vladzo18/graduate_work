using GameState;
using ServiceLocator;
using UIElements;

namespace Menu.MapsShop
{
    public class MapsShopController
    {
        private readonly MapsShopView _mapsShopView;
        private readonly MapsStorage _mapsStorage;
        private readonly IGameState _gameState;

        private int _index;

        public MapsShopController(MapsShopView mapsShopView)
        {
            _mapsShopView = mapsShopView;
            _mapsStorage = _mapsShopView.MapsStorage;
            _gameState = Locator.Inctance.GetService<IGameState>();

            _mapsShopView.NextButton.OnButtonClicked += OnNextButtonClickHandler;
            _mapsShopView.PreviousButton.OnButtonClicked += OnPreviousButtonClickHandler;
            _mapsShopView.ActionButton.OnButtonClick += OnActionButtonClick;

            _index = GetCurrentIndex();
            ChangeMapItem();
            UpdateCoins();
        }

        public void Dispose()
        {
            _mapsShopView.NextButton.OnButtonClicked -= OnNextButtonClickHandler;
            _mapsShopView.PreviousButton.OnButtonClicked -= OnPreviousButtonClickHandler;
            _mapsShopView.ActionButton.OnButtonClick -= OnActionButtonClick;
        }

        private void OnNextButtonClickHandler()
        {
            _index++;

            if (_index >= _mapsStorage._MapsStorageDescriptors.Count)
                _index = 0;
            
            ChangeMapItem();
        }

        private void OnPreviousButtonClickHandler()
        {
            _index--;

            if (_index < 0)
                _index = _mapsStorage._MapsStorageDescriptors.Count - 1;
            
            ChangeMapItem();
        }

        private void OnActionButtonClick(StoreActionButton.StoreActionButtonState state)
        {
            switch (state)
            {
                case StoreActionButton.StoreActionButtonState.Use:
                    _gameState.UserStateData.CurrentMapType = _mapsStorage._MapsStorageDescriptors[_index].MapType;
                    _mapsShopView.ActionButton.SetInter(false);
                    _mapsShopView.ActionButton.MakeUseSound();
                    break;
                case StoreActionButton.StoreActionButtonState.Buy:
                    if (_gameState.UserStateData.PointsAmount >= _mapsStorage._MapsStorageDescriptors[_index].price)
                    {
                        _gameState.UserStateData.PointsAmount -= _mapsStorage._MapsStorageDescriptors[_index].price;
                        _gameState.UserStateData.AvalibleMapTypes.Add(_mapsStorage._MapsStorageDescriptors[_index].MapType);
                       
                        _mapsShopView.Coins = _gameState.UserStateData.PointsAmount;
                        _mapsShopView.MapItemView.SetLock(false);
                        _mapsShopView.ActionButton.SetState(StoreActionButton.StoreActionButtonState.Use);
                        _mapsShopView.ActionButton.MakeBuySound();
                    }
                    else
                    {
                        _mapsShopView.ActionButton.MakeBlockedSound();
                    }
                    break;
            }
        }

        private void ChangeMapItem()
        {
           MapsStorageDescriptor descriptor = _mapsStorage._MapsStorageDescriptors[_index];
           _mapsShopView.MapItemView.SetIcon(descriptor.icon);
           _mapsShopView.MapItemView.SetPrice(descriptor.price);
           _mapsShopView.MapItemView.SetText(descriptor.Name);
           
           bool isAvb = IsAvaliable(descriptor.MapType);
           _mapsShopView.MapItemView.SetLock(!isAvb);
           _mapsShopView.ActionButton.SetState(isAvb ? StoreActionButton.StoreActionButtonState.Use : StoreActionButton.StoreActionButtonState.Buy);

           _mapsShopView.ActionButton.SetInter(true);
           if (descriptor.MapType == _gameState.UserStateData.CurrentMapType)
               _mapsShopView.ActionButton.SetInter(false);
        }

        private bool IsAvaliable(MapType target)
        {
            foreach (MapType mapType in _gameState.UserStateData.AvalibleMapTypes)
                if (target == mapType)
                    return true;
            
            return false;
        }

        private int GetCurrentIndex()
        {
            for (int i = 0; i <  _mapsStorage._MapsStorageDescriptors.Count; i++)
                if (_mapsStorage._MapsStorageDescriptors[i].MapType == _gameState.UserStateData.CurrentMapType)
                    return i;
            
            return -1;
        }

        private void UpdateCoins()
        {
            _mapsShopView.Coins = _gameState.UserStateData.PointsAmount;
        }
    }
}