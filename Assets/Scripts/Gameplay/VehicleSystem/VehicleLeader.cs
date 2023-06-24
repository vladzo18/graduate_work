using AudioSystem;
using InputSystem;
using ServiceLocator;
using UIElements;
using UnityEngine;

namespace Gameplay.VehicleSystem
{
    public class VehicleLeader : MonoBehaviour
    {
        [SerializeField] private VehicleMover _vehicleMover;
        [SerializeField] private Death _death;
        
        private InputService _inputService;
        private TargetObjectCameraFollower _camera;
        private DeathOverlay _deathOverlay;
        private IAudioSystem _audioSystem = null;
        
        private void Start()
        {
            _vehicleMover.StartMovement();
            
            _inputService.SwipeDetector.OnSwipe += OnSwipeHandler;
            _inputService.TouchDetector.OnTouch += OnTouchHandler;
            _inputService.TouchDetector.OnUnTouch += OnUnTouchHandler;
            _deathOverlay.OnFullFade += OnFullFadeHAndler;

            _audioSystem = Locator.Inctance.GetService<IAudioSystem>();
        }

        private void OnDestroy()
        {
            _inputService.SwipeDetector.OnSwipe -= OnSwipeHandler;
            _inputService.TouchDetector.OnTouch -= OnTouchHandler;
            _inputService.TouchDetector.OnUnTouch -= OnUnTouchHandler;
            _deathOverlay.OnFullFade -= OnFullFadeHAndler;
        }

        public void Initialize(InputService inputService, TargetObjectCameraFollower camera, DeathOverlay deathOverlay)
        {
            _camera = camera;
            _inputService = inputService;
            _deathOverlay = deathOverlay;
        }

        private void OnSwipeHandler(SwipeDirection direction)
        {
            switch (direction)
            {
                case SwipeDirection.Right:
                    _vehicleMover.JumpRight();
                    break;
                case SwipeDirection.Left:
                    _vehicleMover.JumpLeft();
                    break;
            }
            
            _audioSystem.PlayAudio(AudioEnum.Swipe);
        }

        private void OnFullFadeHAndler() => 
            _death.MakeDeath();
        
        private void OnTouchHandler()
        {
            _vehicleMover.StopMovement();
            _deathOverlay.StartFade();
        }

        private void OnUnTouchHandler()
        {
            _vehicleMover.StartMovement();
            _deathOverlay.StopFade();
        }
    }
}
