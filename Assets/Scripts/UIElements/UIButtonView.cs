using System;
using AudioSystem;
using ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    [RequireComponent(typeof(Image))]
    public class UIButtonView : MonoBehaviour
    {
        private static readonly int HIT_BUTTON_ANIMATION_HASH = Animator.StringToHash("Hit");

        #if UNITY_EDITOR
        [SerializeField] private Sprite _buttonSprite;
        #endif
        
        [SerializeField] private Button _button;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioEnum _clickSound = AudioEnum.UiButtonClick;

        private IAudioSystem _audioSystem = null;

        public event Action OnButtonClicked;

        private void Start() => 
            _audioSystem = Locator.Inctance.GetService<IAudioSystem>();

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClickHandler);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClickHandler);

        private void OnButtonClickHandler()
        {
            _audioSystem.PlayAudio(_clickSound);
            _animator.SetTrigger(HIT_BUTTON_ANIMATION_HASH);
        }

        private void OnButtonClickEnded_AnimationEvent() => 
            OnButtonClicked?.Invoke();

        #if UNITY_EDITOR
        private void OnValidate() => 
            this.GetComponent<Image>().sprite = _buttonSprite;
        #endif
    }
}
