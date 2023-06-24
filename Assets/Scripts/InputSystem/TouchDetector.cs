using System;
using UnityEngine;

namespace InputSystem
{
    public class TouchDetector
    {
        private const float THRESHOLD = 0.5f;
        private const int COUNTS_DELAY = 45;    
        
        private readonly TouchObserver _touchObserver;
        
        public event Action OnTouch;
        public event Action OnUnTouch;

        private Vector2 _position;

        private int _counter;
        private bool _isHolding;
        private Vector2 _touchPosition;
        private Vector2 _prevTouchPosition;

        public TouchDetector(TouchObserver touchObserver)
        {
            _touchObserver = touchObserver;

            _touchObserver.OnTouchBegan += OnTouchBeganHandler;
            _touchObserver.OnTouchMoved += OnTouchMovedHandler;
            _touchObserver.OnTouchStationary += OnTouchStationaryHandler;
            _touchObserver.OnTouchEnded += OnTouchEndedHandler;
        }

        public void Dispose()
        {
            _touchObserver.OnTouchBegan -= OnTouchBeganHandler;
            _touchObserver.OnTouchMoved -= OnTouchMovedHandler;
            _touchObserver.OnTouchStationary -= OnTouchStationaryHandler;
            _touchObserver.OnTouchEnded -= OnTouchEndedHandler;
        }

        private void OnTouchBeganHandler(Vector2 position)
        {
            _isHolding = true;
            _touchPosition = position;
            _prevTouchPosition = position;
        }

        private void OnTouchStationaryHandler(Vector2 position)
        {
            if (_isHolding)
            {
                _isHolding = false;
                _touchPosition = position;
                _prevTouchPosition = position;
            }
            
            _counter++;

            if (_counter >= COUNTS_DELAY)
                OnTouch.Invoke();
        }

        private void OnTouchMovedHandler(Vector2 position)
        {
            _touchPosition = position;

            if (Vector2.Distance(_touchPosition, _prevTouchPosition) > 0)
            {
                _isHolding = false;
            }
        }

        private void OnTouchEndedHandler(Vector2 position)
        {
            if (!_isHolding)
            {
                _isHolding = false;
                OnUnTouch.Invoke();
            }

            _counter = 0;
        }

        private bool Check(Vector2 position) => 
            Math.Abs(position.x - _position.x) < THRESHOLD && Math.Abs(position.y - _position.y) < THRESHOLD;
    }
}