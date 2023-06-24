using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WindowSystem;

namespace Gameplay.TutorialSystem
{
    public class PromptWindow : BaseWindow
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Animator _animator;

        public event Action OnClick;

        protected override void Start()
        {
            base.Start();
            _button.onClick.AddListener(OnButtonClick);
            this.Hide();
        }

        private void OnDestroy() => 
            _button.onClick.RemoveListener(OnButtonClick);

        public void SetText(string text) => 
            _text.text = text;

        public override void Show()
        {
            base.Show();
            _animator.SetTrigger("Show");
        }

        private void OnButtonClick()
        {
            _animator.SetTrigger("Hide");
            OnClick?.Invoke();
        }

        //Calls from Animator
        private void HideEnd_AnimationKey() => 
            this.Hide();
    }
}