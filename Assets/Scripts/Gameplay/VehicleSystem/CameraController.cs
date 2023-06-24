using Additional;
using UnityEngine;

namespace Gameplay.VehicleSystem
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public Rigidbody _rigidbody;
        
        public Vector3 offset;
        public float smoothTime = 0.3f;
        public float predictionTime = 0.1f;
        public float noise = 0.1f;

        private Vector3 velocity = Vector3.zero;
        private Vector3 predictedPosition;
        private Vector3 filteredPosition;

        private KalmanFilter kalmanFilter;

        void Start()
        {
            kalmanFilter = new KalmanFilter(noise);
        }

        public void Initialize(Transform taransform, Rigidbody rigidbody)
        {
            target = taransform;
            _rigidbody = rigidbody;
        }
        
        void FixedUpdate()
        {
            if (target == null)
            {
                return;
            }
            
            // Get the velocity of the target (assuming it has a Rigidbody component)
            Vector3 targetVelocity = _rigidbody.velocity;

            // Predict the position of the target using its current velocity
            predictedPosition = target.position + targetVelocity * predictionTime;

            // Filter the predicted position using a Kalman filter
            filteredPosition = kalmanFilter.Filter(predictedPosition);

            // Add the offset to the filtered position
            Vector3 targetPosition = filteredPosition + offset;

            // Smoothly move the camera to the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}