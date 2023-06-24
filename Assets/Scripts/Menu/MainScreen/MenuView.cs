using UIElements;
using UnityEngine;

namespace Menu.MainScreen
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private ViewType _viewType;
        [SerializeField] private UIButtonView _playButton;
        [SerializeField] private UIButtonView _storeButton;
        [SerializeField] private UIButtonView _settingsButton;
        [SerializeField] private UIButtonView _ratingButton;
        [SerializeField] private Canvas _menuCanvas;
        
        public UIButtonView PlayButton => _playButton;
        public UIButtonView StoreButton => _storeButton;
        public UIButtonView SettingsButton => _settingsButton;
        public UIButtonView RatingButton => _ratingButton;
        
        public void SetVisibility(bool status) =>
            _menuCanvas.enabled = status;
    }
}