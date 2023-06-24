using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
    public class DeathOverlay : MonoBehaviour
    {
        [SerializeField] private Image _overlayImage;

        private const float FadeSpeed = 0.5f;
        private const float UnFadeSpeed = 3f;
        
        private bool _canFade = false;
        private bool _canInvoke = false;
        private bool _isFading = false;

        public event Action OnFullFade;
        
        private void Update()
        {
            if (!_canFade) return;

            if (_isFading)
            {
                Color color = _overlayImage.color;
                color.a += (FadeSpeed * Time.deltaTime);
                _overlayImage.color = color;

                if (_overlayImage.color.a >= 1 && _canFade)
                {
                    _canFade = false;

                    if (_canInvoke)
                    {
                        _canInvoke = false;
                        OnFullFade?.Invoke();
                    }
                }
            }
            else
            {
                Color color = _overlayImage.color;
                color.a -= (UnFadeSpeed * Time.deltaTime);
                _overlayImage.color = color;

                if (_overlayImage.color.a <= 0 && _canFade)
                    _canFade = false;
            }
        }

        public void StartFade()
        {
            _canFade = true;
            _isFading = true;
        }

        public void StopFade()
        {
            _canFade = true;
            _canInvoke = true;
            _isFading = false;
        }
    }
}