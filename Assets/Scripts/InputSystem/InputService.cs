using UnityEngine;

namespace InputSystem
{
    public class InputService : MonoBehaviour
    {
        private TouchObserver _touchObserver;
        private SwipeDetector _swipeDetector;
        private TouchDetector _touchDetector;

        public SwipeDetector SwipeDetector => _swipeDetector;
        public TouchDetector TouchDetector => _touchDetector;
        
        private void Awake()
        {
            _touchObserver = new TouchObserver();
            _swipeDetector = new SwipeDetector(_touchObserver);
            _touchDetector = new TouchDetector(_touchObserver);
        }

        private void OnDestroy()
        {
            _swipeDetector.Dispose();
            _touchDetector.Dispose();
        }

        private void Update() => 
            _touchObserver.Update();
    }
}