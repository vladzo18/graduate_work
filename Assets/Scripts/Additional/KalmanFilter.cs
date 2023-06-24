using UnityEngine;

namespace Additional
{
    public class KalmanFilter
    {
        private float q;
        private float r;
        private float x;
        private float p;

        public KalmanFilter(float noise)
        {
            q = noise; // Process noise covariance
            r = noise; // Measurement noise covariance
            x = 0; // Initial state estimate
            p = 1; // Initial state covariance
        }

        public Vector3 Filter(Vector3 measurement)
        {
            // Predict the next state
            float x_pred = x;
            float p_pred = p + q;

            // Update the state estimate based on the measurement
            float k = p_pred / (p_pred + r);
            x = x_pred + k * (measurement.x - x_pred);
            p = (1 - k) * p_pred;

            return new Vector3(x, measurement.y, measurement.z);
        }
    }
}