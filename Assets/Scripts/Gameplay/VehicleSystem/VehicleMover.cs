using System.Collections;
using Gameplay.ComplexitySystem;
using Gameplay.Movement;
using Gameplay.PauseSystem;
using ServiceLocator;
using UnityEngine;

namespace Gameplay.VehicleSystem
{
    public class VehicleMover : MonoBehaviour, IPausable, IComplexityReactor
    {
        [SerializeField] private float _baseSpeed;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _jumpDuration = 0.2f;

        private bool _isJumping;
        private bool _canMove;
        private MovementAxis _movementAxis;

        public bool IsJumping => _isJumping;

        private void Start()
        {
            _movementAxis = MovementAxis.GetDefaultAxis();
            
            Locator.Inctance.GetService<IPauseSystem>().RegisterPausable(this);
            Complexity.Instance.SetComplexityReactor(this);
            
            StopMovement();
        }

        private void OnDestroy()
        {
            Locator.Inctance.GetService<IPauseSystem>().UnRegisterPausable(this);
        }

        private void FixedUpdate()
        {
            if (!_canMove)
                return;
            
            //_rigidbody.velocity = new Vector3(0, 0, _baseSpeed);
            
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            _rigidbody.MovePosition( _rigidbody.position + forward * (_baseSpeed * Time.deltaTime));
        }

        public void JumpRight()
        {
            if(_isJumping || _movementAxis.CheckRightDisplacement())
                return;
            
            _isJumping = true;

            StartCoroutine(JumpRoutine(_movementAxis.CurrentCoordinate, _movementAxis.DisplaceRight(), _jumpDuration));
        }

        public void JumpLeft()
        {
            if(_isJumping || _movementAxis.CheckLeftDisplacement())
                return;
            
            _isJumping = true;
            
            StartCoroutine(JumpRoutine(_movementAxis.CurrentCoordinate, _movementAxis.DisplaceLeft(), _jumpDuration));
        }

        public void StartMovement() => 
            _canMove = true;

        public void StopMovement()
        {
            _rigidbody.velocity = Vector3.zero;
            _canMove = false;
        }

        private IEnumerator JumpRoutine(float originX, float targetX, float duration)
        {
            WaitForFixedUpdate waitForFixedUpdateCall = new WaitForFixedUpdate();
            float journey = 0f;
            
            while (journey <= duration)
            {
                journey += Time.fixedDeltaTime;

                float newX = Mathf.Lerp(originX, targetX, journey / duration);
                _rigidbody.position = new Vector3(newX, _rigidbody.position.y, _rigidbody.position.z);

                yield return waitForFixedUpdateCall;
            }

            _isJumping = false;
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

        public void ReactorOnComplexityChange(int value)
        {
            Debug.Log(value);
            _baseSpeed += value * 2;
        }
    }
}