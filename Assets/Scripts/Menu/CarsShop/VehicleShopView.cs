using Gameplay;
using TMPro;
using UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.CarsShop
{
    public class VehicleShopView : MonoBehaviour
    {
        [SerializeField] private ViewType _viewType;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private UIButtonView _backButton;
        [SerializeField] private Image _vehicleIcon;
        [SerializeField] private UIButtonView _nextButton;
        [SerializeField] private UIButtonView _previousButton;
        [SerializeField] private Button _actionButton;
        [SerializeField] private TMP_Text _coinText;
        [SerializeField] private Image _lockImage;
        [SerializeField] private VehicleShopStorage vehicleShopStorage;
        [SerializeField] private VehiclePedestal _vehiclePedestal;
        [SerializeField] private StoreActionButton _storeActionButton;
        [SerializeField] private GameObject _priceBox;
        [SerializeField] private TMP_Text _preiceText;
        [SerializeField] private TMP_Text _carNameText;
        
        public UIButtonView BackButton => _backButton;
        public UIButtonView NextButton => _nextButton;
        public UIButtonView PreviousButton => _previousButton;
        public VehicleShopStorage VehicleShopStorage => vehicleShopStorage;
        public StoreActionButton StoreActionButton => _storeActionButton;
        public VehiclePedestal VehiclePedestal => _vehiclePedestal;

        public void SetCoinAmount(int amount) => 
            _coinText.text = amount.ToString();

        public void SetVehicleIcon(Sprite icon) =>
            _vehicleIcon.sprite = icon;

        public void SetLock(bool status) => 
            _lockImage.enabled = status;

        public void PriceBox(bool v, int p)
        {
            _priceBox.SetActive(v);
            _preiceText.text = p.ToString();
        }

        public void SetCarNameText(string name) =>
            _carNameText.text = name;
        
        public void SetVisibility(bool status) => 
            _canvas.enabled = status;
    }
}