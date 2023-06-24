using System.Collections;
using UnityEngine;

namespace Gameplay.VehicleSystem
{
   [RequireComponent(typeof(Camera))]
   public class TargetObjectCameraFollower : MonoBehaviour
   {
      [SerializeField] private Transform _targetTransform;
      [SerializeField] private float _lerpRate;
      [SerializeField] private float _yOffset = 4f;
      [SerializeField] private float _zOffset = -4f;
      

      
      public Transform target; // Цель, за которой следует камера
      public float smoothSpeed = 0.125f; // Скорость перемещения камеры
      public Vector3 offset; // С
      
      private bool _isInEffect;
      private Vector3 _cameraPosition;
      
      private void FixedUpdate()
      {
         //_cameraTransform.position = CalculateCameraPositionOnlyInZAxis();
         transform.position = Vector3.Lerp(transform.position, CalculateCameraPositionOnlyInZAxis(), Time.fixedDeltaTime * _lerpRate);
         //_cameraPosition = _cameraTransform.position;
         
         //Vector3 desiredPosition = target.position + offset;
         //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.fixedDeltaTime * smoothSpeed);
         //transform.position = smoothedPosition;
         //transform.LookAt(target);
      }
      
      #if UNITY_EDITOR
      private void OnValidate()
      {
         //transform.position = CalculateCameraPosition();
         //transform.position = target.position + offset;
      }
      
      private Vector3 CalculateCameraPosition()
      {
         return new Vector3(
            _targetTransform.position.x, 
            _targetTransform.position.y + _yOffset, 
            _targetTransform.position.z + _zOffset
         );
      }
      #endif
      
      public void MakeInclineEffect()
      {
         if(_isInEffect)
            return;
         _isInEffect = true;
         
         //StartCoroutine(InclineEffectRoutine());
      }
      
      private Vector3 CalculateCameraPositionOnlyInZAxis()
      {
         return new Vector3(
            transform.position.x, 
            transform.position.y, 
            _targetTransform.position.z + _zOffset
         );
      }

      private IEnumerator InclineEffectRoutine()
      {
         float duration = 0.25f;
         float journey = 0f;
         
         float currentRotationX = this.transform.rotation.eulerAngles.x;
         float currentPositionY = this.transform.position.y;
         float targetRotationX = currentRotationX + 2;
         float targetPositionY = currentPositionY + 2;
         
         yield return StartCoroutine(AnimateInclineEffect(currentRotationX, targetRotationX, currentPositionY, targetPositionY, duration));
         yield return StartCoroutine(AnimateInclineEffect(targetRotationX, currentRotationX, targetPositionY, currentPositionY, duration));
   
         _isInEffect = false;
      }

      private IEnumerator AnimateInclineEffect(float startRotationX, float endRotationX, float startPosY, float endPosY, float duration)
      {
         float journey = 0f;
         
         while (journey <= duration)
         {
            journey += Time.deltaTime;

            float rotationX = Mathf.Lerp(startRotationX, endRotationX, journey / duration);
            float newY = Mathf.Lerp(startPosY, endPosY, journey / duration);

            this.transform.rotation = Quaternion.Euler(rotationX, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
            this.transform.position = new Vector3(this.transform.position.x, newY, this.transform.position.z);

            yield return null;
         }
      }
      
   }
}
