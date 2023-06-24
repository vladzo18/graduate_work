using UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.ShopSwitch
{
    public class ShopSwitchView : MonoBehaviour
    {
        [SerializeField] private ViewType _viewType;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _mapsButton;
        [SerializeField] private Button _carsButton;
        [SerializeField] private UIButtonView _backButtonView;
        
        public Button MapsButton => _mapsButton;
        public Button CarsButton => _carsButton;
        public UIButtonView BackButtonView => _backButtonView;

        public void SetVisibility(bool status) => 
            _canvas.enabled = status;
    }
}