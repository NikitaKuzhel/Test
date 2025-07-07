using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        private Transform _target;
        private bool _canMove;

        public void InitTargetToMove(Transform target)
        {
            _target = target;
        }
        
        public void StartMovement()
        {
            _canMove = true;
        }

        public void StopMovement()
        {
            _canMove = false;
        }
        
        private void Update()
        {
            if (_canMove == false) return;
            
            transform.position = Vector3.MoveTowards(
                transform.position,
                _target.position,
                _moveSpeed * Time.deltaTime
            );
            
            transform.LookAt(_target);
        }
    }
}