using UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace WindowSystem
{
    public class PauseWindow : BaseWindow
    {
        [SerializeField] private Image _overlayImage;
        [SerializeField] private UIButtonView _playButton;
        [SerializeField] private UIButtonView _backToMenuButton;
        [SerializeField] private Animator _animator;
        
        public UIButtonView PlayButton => _playButton;
        public UIButtonView BackToMenuButton => _backToMenuButton;

        private void OnEnable()
        {
            //_playButton.OnButtonClicked += 
            //_playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
            //_backToMenuButton.onClick.AddListener(() => OnBackButtonClicked?.Invoke());
        }

        private void OnDisable()
        {
            //_playButton.onClick.RemoveAllListeners();
            //_backToMenuButton.onClick.RemoveAllListeners();
        }

        public override void Show()
        {
            base.Show();
            _animator.SetTrigger("down");
        }

        public override void Hide() => 
            _animator.SetTrigger("up");

        //Calls from Animator
        private void Up_AnimationKey() => 
            base.Hide();
    }
}