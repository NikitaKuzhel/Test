using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerView : MonoBehaviour
    {
        public event Action<int> OnHitPlayer;
        
        [SerializeField] private PlayerMovementBehaviour _playerMovementBehaviour;
        [SerializeField] private TurretBehaviour _turretBehaviour;
        [SerializeField] private AttackBehaviour _attackBehaviour;
        [SerializeField] private PlayerHealthBar _healthBar;

        public void InitInputBehaviour(InputBehaviour inputBehaviour)
        {
            _turretBehaviour.InitInputBehaviour(inputBehaviour);
        }
        
        public void InitPoolManager(PoolManager<BulletBehaviour> poolManager)
        {
            _attackBehaviour.InitPoolManager(poolManager);
        }
        
        public void SetStartPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void StartMovement()
        {
            _playerMovementBehaviour.StartMovement();
            _attackBehaviour.StartShooting();
        }

        public void StopMovement()
        {
            _playerMovementBehaviour.StopMovement();
            _attackBehaviour.StopShooting();
        }

        public void TakeDamage(int damage)
        {
            OnHitPlayer.Invoke(damage);
        }
        
        public void SetMaxHealth(int maxHealth)
        {
            _healthBar.SetMaxHealth(maxHealth);
        }

        public void SetCurrentHealth(int health)
        {
            _healthBar.SetCurrentHealth(health);
        }
    }
}