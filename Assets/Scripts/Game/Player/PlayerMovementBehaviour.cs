using UnityEngine;

namespace Game.Player
{
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        
        private bool _isMoving = false;
        
        public void StartMovement()
        {
            _isMoving = true;
        }

        public void StopMovement()
        {
            _isMoving = false;
        }

        private void Update()
        {
            if (_isMoving == false) return;
            transform.position += Vector3.forward * (_moveSpeed * Time.deltaTime);
        }
    }
}