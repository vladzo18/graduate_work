using AudioSystem;
using ServiceLocator;

namespace Menu
{
    public class ViewsController
    {
        private readonly Views _views;
        private IAudioSystem _audioSystem;

        private ViewType _lastView;
        
        public ViewsController(Views views) => 
            _views = views;

        public void Initialize()
        {
            _audioSystem = Locator.Inctance.GetService<IAudioSystem>();
            
            _views.VehicleShopView.BackButton.OnButtonClicked += OnBackButtonClickHandler;
            _views.RatingView.BackButton.OnButtonClicked += OnBackButtonClickHandler;
            _views.SettingsView.BackButton.OnButtonClicked += OnBackButtonClickHandler;
            _views.MenuView.RatingButton.OnButtonClicked += OnRatingButtonClickHandler;
            _views.MenuView.SettingsButton.OnButtonClicked += OnSettingsButtonClickHandler;
            _views.MenuView.StoreButton.OnButtonClicked +=  OnStoreButtonClickHandler;
            _views.ShopSwitchView.CarsButton.onClick.AddListener(OnCarsButtonClickHandler);
            _views.ShopSwitchView.MapsButton.onClick.AddListener(OnMapsButtonClickHandler);
            _views.ShopSwitchView.BackButtonView.OnButtonClicked += OnBackButtonClickHandler;
            _views.MapsShopView.BackButtonView.OnButtonClicked += OnBackButtonClickHandler;
            
        }

        public void Dispose()
        {
            _views.VehicleShopView.BackButton.OnButtonClicked -= OnBackButtonClickHandler;
            _views.RatingView.BackButton.OnButtonClicked -= OnBackButtonClickHandler;
            _views.SettingsView.BackButton.OnButtonClicked -= OnBackButtonClickHandler;
            _views.MenuView.RatingButton.OnButtonClicked -= OnRatingButtonClickHandler;
            _views.MenuView.SettingsButton.OnButtonClicked -= OnSettingsButtonClickHandler;
            _views.MenuView.StoreButton.OnButtonClicked -= OnStoreButtonClickHandler;
            _views.ShopSwitchView.CarsButton.onClick.RemoveListener(OnCarsButtonClickHandler);
            _views.ShopSwitchView.MapsButton.onClick.RemoveListener(OnMapsButtonClickHandler);
            _views.ShopSwitchView.BackButtonView.OnButtonClicked -= OnBackButtonClickHandler;
            _views.MapsShopView.BackButtonView.OnButtonClicked -= OnBackButtonClickHandler;
        }

        private void OnCarsButtonClickHandler()
        {
            _views.ShopSwitchView.SetVisibility(false);
            _views.VehicleShopView.SetVisibility(true);
            _audioSystem.PlayAudio(AudioEnum.PageSound);
            _lastView = ViewType.ShopSwitch;
        }

        private void OnMapsButtonClickHandler()
        {
            _views.ShopSwitchView.SetVisibility(false);
            _views.MapsShopView.SetVisibility(true);
            _audioSystem.PlayAudio(AudioEnum.PageSound);
            _lastView = ViewType.ShopSwitch;
        }

        private void OnStoreButtonClickHandler()
        {
            _views.MenuView.SetVisibility(false);
            _views.ShopSwitchView.SetVisibility(true);
            _lastView = ViewType.MainScreen;
        }

        private void OnSettingsButtonClickHandler()
        {
            _views.MenuView.SetVisibility(false);
            _views.SettingsView.SetVisibility(true);
            _lastView = ViewType.MainScreen;
        }

        private void OnRatingButtonClickHandler()
        {
            _views.MenuView.SetVisibility(false);
            _views.RatingView.SetVisibility(true);
            _lastView = ViewType.MainScreen;
        }

        private void OnBackButtonClickHandler()
        {

            switch (_lastView)
            {
                case ViewType.MainScreen:
                    _views.RatingView.SetVisibility(false);
                    _views.VehicleShopView.SetVisibility(false);
                    _views.SettingsView.SetVisibility(false);
                    _views.MapsShopView.SetVisibility(false);
                    _views.ShopSwitchView.SetVisibility(false);
                    _views.MenuView.SetVisibility(true);
                    break;
                case ViewType.ShopSwitch:
                            
                    _views.RatingView.SetVisibility(false);
                    _views.VehicleShopView.SetVisibility(false);
                    _views.SettingsView.SetVisibility(false);
                    _views.MapsShopView.SetVisibility(false);
                    _views.ShopSwitchView.SetVisibility(true);
                    break;
            }
            
            _lastView = ViewType.MainScreen;
        }
        
    }

    public enum ViewType
    {
        None,
        MainScreen,
        Settings,
        Rating,
        ShopSwitch,
        CarShop,
        MapShop
    }
    
}