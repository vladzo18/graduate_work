using System.Collections;
using Additional;
using Gameplay.Movement;
using Gameplay.PauseSystem;
using ServiceLocator;
using UnityEngine;

namespace Gameplay.EnemyCarSystem
{
    public class EnemyCarMover : MonoBehaviour, IPausable
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ObstacleChecker _obstacleChecker;

        private bool _canMove;
        private MovementAxis _movementAxis;

        private void Start() => 
            Locator.Inctance.GetService<IPauseSystem>().RegisterPausable(this);

        private void OnDestroy() => 
            Locator.Inctance.GetService<IPauseSystem>().UnRegisterPausable(this);

        private void OnEnable() => 
            _obstacleChecker.OnObstacleObserved += OnObstacleObservedHandler;

        private void OnDisable() => 
            _obstacleChecker.OnObstacleObserved -= OnObstacleObservedHandler;

        private void FixedUpdate()
        {
            if (!_canMove)
                return;
            
            _rigidbody.velocity = new Vector3(0, 0, -_speed);
        }

        public void StartMovement() => 
            _canMove = true;

        public void StopMovement()
        {
            _rigidbody.velocity = Vector3.zero;
            _canMove = false;
        }

        public void SetMovementAxis(Vector3 axis)
        {
            this.transform.position = axis;
            _movementAxis = new MovementAxis(0, 3.35f, 1, axis.x);
        }

        private void OnObstacleObservedHandler() => 
            CoroutineStarter.Start(JumpToRoutine(_movementAxis.CurrentCoordinate, _movementAxis.DisplaceToFree(), 0.2f));

        private IEnumerator JumpToRoutine(float originX, float targetX, float duration)
        {
            WaitForFixedUpdate waitForFixedUpdateCall = new WaitForFixedUpdate();
            float journey = 0f;
            
            StopMovement();
            
            while (journey <= duration)
            {
                journey += Time.fixedDeltaTime;

                float newX = Mathf.Lerp(originX, targetX, journey / duration);
                _rigidbody.position = new Vector3(newX, _rigidbody.position.y, _rigidbody.position.z);

                yield return waitForFixedUpdateCall;
            }
            
            StartMovement();
        }

        public void SetPaused(bool status)
        {
            if (status)
            {
                StopMovement();
                return;
            }
            
            StartMovement();
        }
    }
}
