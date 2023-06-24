using System;
using TMPro;
using UIElements;
using UnityEngine;

namespace Menu.MapsShop
{
    public class MapsShopView : MonoBehaviour
    {
        [SerializeField] private ViewType _viewType;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private UIButtonView _backButtonView;
        [SerializeField] private StoreActionButton _actionButton;
        [SerializeField] private UIButtonView _nextButton;
        [SerializeField] private UIButtonView _previousButton;
        [SerializeField] private MapItemView _mapItemView;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private MapsStorage _mapsStorage;

        public UIButtonView BackButtonView => _backButtonView;
        public StoreActionButton ActionButton => _actionButton;
        public UIButtonView NextButton => _nextButton;
        public UIButtonView PreviousButton => _previousButton;
        public MapItemView MapItemView => _mapItemView;
        public MapsStorage MapsStorage => _mapsStorage;
        
        public int Coins
        {
            get => 
                Convert.ToInt32(_coinsText.text);
            set =>
                _coinsText.text = Mathf.Clamp(value, 0, Int32.MaxValue).ToString();
        }
        
        public void SetVisibility(bool status) =>
            _canvas.enabled = status;
    }
}