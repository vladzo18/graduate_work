using System;
using UnityEngine;

namespace InputSystem
{
    public class SwipeDetector
    {
        private const float SWIPE_THRESHOLD = 20f;
        
        private readonly TouchObserver _touchObserver;

        private Vector2 _fingerDown;
        private Vector2 _fingerUp;

        public event Action<SwipeDirection> OnSwipe;
        
        public SwipeDetector(TouchObserver touchObserver)
        {
            _touchObserver = touchObserver;

            _touchObserver.OnTouchBegan += OnTouchBeganHandler;
            _touchObserver.OnTouchEnded += OnTouchEndedHandler;
        }

        public void Dispose()
        { 
            _touchObserver.OnTouchBegan -= OnTouchBeganHandler;
            _touchObserver.OnTouchEnded -= OnTouchEndedHandler;
        }

        private void OnTouchBeganHandler(Vector2 position)
        {
            _fingerDown = position;
        }

        private void OnTouchEndedHandler(Vector2 position)
        {
            _fingerUp = position;
            CheckSwipe();
        }
        
        private void CheckSwipe()
        {
            if(!IsHorizontalSwipe()) 
                return;
        
            if (_fingerDown.x - _fingerUp.x > 0)
                OnSwipe?.Invoke(SwipeDirection.Left);
            
            else if (_fingerDown.x - _fingerUp.x < 0)
                OnSwipe?.Invoke(SwipeDirection.Right);
            
            _fingerUp = _fingerDown = Vector2.zero;
        }

        private bool IsHorizontalSwipe() => 
            HorizontalSwipeDistance() > SWIPE_THRESHOLD && HorizontalSwipeDistance() > VerticalSwipeDistance();
        
        private float HorizontalSwipeDistance() =>
            Mathf.Abs(_fingerDown.x - _fingerUp.x);
        
        private float VerticalSwipeDistance() =>
            Mathf.Abs(_fingerDown.y - _fingerUp.y);
        
    }
}