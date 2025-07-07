using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Player
{
    public interface IPlayerController : IPlayerHealthController
    {
        void InitView(PlayerView view);
        void InitInputBehaviour(InputBehaviour inputBehaviour);
        void InitPoolManager(PoolManager<BulletBehaviour> poolManager);
        void SetStartPosition(Vector3 position);
        void SetStartHealth();
        void StartMovement();
        void StopMovement();
        void Subscribe();
        void UnSubscribe();
        void Destroy();
    }

    public class PlayerController :  IPlayerController
    {
        public int MaxHealth => _playerHealthController.MaxHealth;
        public int CurrentHealth =>  _playerHealthController.CurrentHealth;
        public event Action<int> OnHealthChanged;
        public event Action OnDeath;
        
        private readonly IPlayerHealthController _playerHealthController;
        private PlayerView _playerView;

        public PlayerController(IPlayerHealthController playerHealthController)
        {
            _playerHealthController = playerHealthController;
        }

        public void InitView(PlayerView view)
        {
            _playerView =  view;
        }
        
        public void InitInputBehaviour(InputBehaviour inputBehaviour)
        {
            _playerView.InitInputBehaviour(inputBehaviour);
        }
        
        public void InitPoolManager(PoolManager<BulletBehaviour> poolManager)
        {
            _playerView.InitPoolManager(poolManager);
        }
        
        public void SetStartHealth()
        {
            _playerHealthController.SetStartHealth();
            _playerView.SetMaxHealth(_playerHealthController.MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            _playerHealthController.TakeDamage(damage);
        }

        public void SetStartPosition(Vector3 position)
        {
            _playerView.SetStartPosition(position);
        }

        public void StartMovement()
        {
            _playerView.StartMovement();
        }

        public void StopMovement()
        {
            _playerView.StopMovement();
        }

        public void Subscribe()
        {
            _playerHealthController.OnHealthChanged += OnHealthChangedListener;
            _playerHealthController.OnDeath += OnDeathListener;
            _playerView.OnHitPlayer += OnTakeDamage;
        }

        public void UnSubscribe()
        {
            _playerHealthController.OnHealthChanged -= OnHealthChangedListener;
            _playerHealthController.OnDeath -= OnDeathListener;
            _playerView.OnHitPlayer -= OnTakeDamage;
        }
        
        public void Destroy()
        {
            Object.Destroy(_playerView.gameObject);
        }

        private void OnHealthChangedListener(int health)
        {
            OnHealthChanged?.Invoke(health);
        }

        private void OnDeathListener()
        {
            OnDeath?.Invoke();
        }

        private void OnTakeDamage(int damage)
        {
            _playerHealthController.TakeDamage(damage);
            _playerView.SetCurrentHealth(_playerHealthController.CurrentHealth);
        }
    }
}