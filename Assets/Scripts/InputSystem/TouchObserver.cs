using System;
using UnityEngine;

namespace InputSystem
{
    public class TouchObserver
    {
        public event Action<Vector2> OnTouchBegan;
        public event Action<Vector2> OnTouchMoved;
        public event Action<Vector2> OnTouchStationary;
        public event Action<Vector2> OnTouchEnded;

        public void Update()
        {
            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchBegan?.Invoke(touch.position);
                    break;
                case TouchPhase.Moved:
                    OnTouchMoved?.Invoke(touch.position);
                    break;
                case TouchPhase.Stationary:
                    OnTouchStationary?.Invoke(touch.position);
                    break;
                case TouchPhase.Ended:
                    OnTouchEnded?.Invoke(touch.position);
                    break;
            }
        }
    }
}