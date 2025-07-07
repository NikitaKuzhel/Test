using UnityEngine;

namespace Game.Player
{
    public class TurretBehaviour : MonoBehaviour
    {
        private const float LeftMaxPoint = -45f;
        private const float RightMaxPoint = 45f;
        
        [SerializeField]private Transform _turretTransform;
        private InputBehaviour _inputBehaviour;
        private float _startLerpPoint;

        public void InitInputBehaviour(InputBehaviour inputBehaviour)
        {
            _inputBehaviour = inputBehaviour;
            _inputBehaviour.InputStart += StartMovementHandler;

        }

        private void OnDestroy()
        {
            _inputBehaviour.InputStart -= StartMovementHandler;
        }

        private void Update()
        {
            if (_inputBehaviour?.IsMoved == true)
            {
                SideMove();
            }
        }

        private void StartMovementHandler()
        {
            _startLerpPoint = (_turretTransform.position.x - LeftMaxPoint) / (RightMaxPoint - LeftMaxPoint);
        }
        
        private void SideMove()
        {
            // if (_inputBehaviour.IsMoved)
            // {
            //     var lerpValue = _startLerpPoint + _inputBehaviour.CurrentLerpDiff;
            //     float lerpPosX = Mathf.Lerp(LeftMaxPoint, RightMaxPoint, lerpValue);
            //
            //     //_turretTransform.position = new Vector3(lerpPosX, _turretTransform.position.y, _turretTransform.position.z);
            //     _turretTransform.rotation = Quaternion.Euler(lerpPosX, _turretTransform.rotation.y, _turretTransform.rotation.z);
            // }
            
            if (_inputBehaviour.IsMoved)
            {
                var lerpValue = _startLerpPoint + _inputBehaviour.CurrentLerpDiff;
                float clampedLerp = Mathf.Clamp01(lerpValue);
                float newRotationY = Mathf.Lerp(LeftMaxPoint, RightMaxPoint, clampedLerp);

                Vector3 currentEuler = _turretTransform.localEulerAngles;
                _turretTransform.localEulerAngles = new Vector3(currentEuler.x, newRotationY, currentEuler.z);
            }
        }
    }
}