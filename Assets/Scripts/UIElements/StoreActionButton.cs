using System;
using AudioSystem;
using ServiceLocator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    public class StoreActionButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private StateSetup[] _stateSetups;

        public event Action<StoreActionButtonState> OnButtonClick;

        private StoreActionButtonState _current = StoreActionButtonState.None;
        private IAudioSystem _audioSystem;

        private void Start()
        {
            _button.onClick.AddListener(hahaha);
            _audioSystem = Locator.Inctance.GetService<IAudioSystem>();
        }

        private void hahaha()
        {
            OnButtonClick?.Invoke(_current);
        }
         
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void SetState(StoreActionButtonState state)
        {
            _current = state;

            foreach (var setup in _stateSetups)
            {
                if (setup.State == _current)
                {
                    _image.color = setup.bgColor;
                    _text.text = setup.buttonText;
                }
            }
        }

        public void MakeUseSound() => 
            _audioSystem.PlayAudio(AudioEnum.TakeSound);
        
        public void MakeBuySound() => 
            _audioSystem.PlayAudio(AudioEnum.Purchase);
        
        public void MakeBlockedSound() => 
            _audioSystem.PlayAudio(AudioEnum.BlockedAction);
        
        public void SetInter(bool staus) =>
            _button.interactable = staus;
        
        public enum StoreActionButtonState
        {
            None,
            Use,
            Buy
        }
        
        [Serializable]
        public class StateSetup
        {
            public StoreActionButtonState State;
            public string buttonText;
            public Color bgColor;
        }
    }
}