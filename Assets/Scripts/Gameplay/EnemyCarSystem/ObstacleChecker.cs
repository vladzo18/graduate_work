using System;
using Gameplay.RoadSystem.ObstacleSubsystem;
using UnityEngine;

namespace Gameplay.EnemyCarSystem
{
    [RequireComponent(typeof(Collider))]
    public class ObstacleChecker : MonoBehaviour
    {
        public event Action OnObstacleObserved;

        private void OnTriggerEnter(Collider other)
        {
            Barrier barrier = other.GetComponent<Barrier>();

            if (barrier)
                OnObstacleObserved?.Invoke();
        }
    }
}