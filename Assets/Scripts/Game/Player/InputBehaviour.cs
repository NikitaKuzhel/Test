using System;
using System.Collections;
using UnityEngine;

namespace Game.Player
{
    public class InputBehaviour : MonoBehaviour
    {
        public Action InputStart;
        public float CurrentLerpDiff => _currentLerpDiff;
        public bool IsMoved { get; private set; }

        public Vector3 MotionVector => _joystickVector;
        
        private const float MovementMultiplier = 1.4f;
        private Coroutine _inputCoroutine;
        private float _startLerpValue;
        private float _currentLerpDiff;
        private Vector3 _joystickVector;

        public void LaunchInputCoroutine()
        {
            _inputCoroutine = StartCoroutine(InputCoroutine());
        }

        public void StopInputCoroutine()
        {
            if (_inputCoroutine != null) StopCoroutine(_inputCoroutine);
        }

        private IEnumerator InputCoroutine()
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    InputStart?.Invoke();
                    _startLerpValue = Input.mousePosition.x / Screen.width;
                }
                else if (Input.GetMouseButton(0))
                {
                    IsMoved = true;
                    _currentLerpDiff = (Input.mousePosition.x / Screen.width - _startLerpValue) * MovementMultiplier;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    IsMoved = false;
                }
                else
                {
                    _currentLerpDiff = 0;
                }
                yield return null;
            }
        }
    }
}