using Menu.CarsShop;
using Menu.MainScreen;
using Menu.MapsShop;
using Menu.RatingScreen;
using Menu.SettingsScreen;
using Menu.ShopSwitch;
using UnityEngine;

namespace Menu
{
    public class Views : MonoBehaviour
    {
        [SerializeField] private MenuView _menuView;
        [SerializeField] private ShopSwitchView _shopSwitchView;
        [SerializeField] private RatingView _ratingView;
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private MapsShopView _mapsShopView;
        [SerializeField] private VehicleShopView vehicleShopView;

        public MenuView MenuView => _menuView;
        public ShopSwitchView ShopSwitchView => _shopSwitchView;
        public RatingView RatingView => _ratingView;
        public SettingsView SettingsView => _settingsView;
        public MapsShopView MapsShopView => _mapsShopView;
        public VehicleShopView VehicleShopView => vehicleShopView;
    }
}