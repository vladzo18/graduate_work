using UnityEngine;

namespace WindowSystem
{
    [RequireComponent(typeof(Canvas))]
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {
        private bool _isVisible;
        private Canvas _canvas;
        
        public bool IsVisible => _isVisible;

        protected virtual void Start() => 
            _canvas = this.GetComponent<Canvas>();

        public virtual void Show() => 
            SetVisibility(true);

        public virtual void Hide() => 
            SetVisibility(false);

        private void SetVisibility(bool isVisible)
        {
            _isVisible = isVisible;
            
            if (_canvas != null)
                _canvas.enabled = _isVisible;
        }
    }
}