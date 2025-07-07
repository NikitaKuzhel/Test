using System;
using System.Collections;
using Game.Player;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public event Action<EnemyController> OnDeath;
        
        [SerializeField] private LookForPlayerDetector _lookForPlayerDetector;
        [SerializeField] private HitPlayerDetector _hitPlayerDetector;
        [SerializeField] private EnemyMovementBehaviour _movementBehaviour;
        [SerializeField] private EnemyStateBehaviour _stateBehaviour;
        [SerializeField] private EnemyAnimationController _animationController;
        [SerializeField] private ParticleSystem _deathParticle;
        [SerializeField] private GameObject _meshObject;
        [SerializeField] private int _damageAmount = 5;
        [SerializeField] private float _deathDelay = 1;
        
        private PlayerView _playerView;
        private Coroutine _deathCoroutine;

        public void Activate()
        {
            SwitchToIdle();
        }

        public void Subscribe()
        {
            _lookForPlayerDetector.OnFindPlayer += OnFindPlayerListener;
            _hitPlayerDetector.OnHitPlayer += OnHitPlayerListener;
        }

        public void Unsubscribe()
        {
            _lookForPlayerDetector.OnFindPlayer -= OnFindPlayerListener;
            _hitPlayerDetector.OnHitPlayer -= OnHitPlayerListener;
        }

        public void Die()
        {
            SwitchToDie();
        }

        private void OnFindPlayerListener(PlayerView playerView)
        {
            _playerView = playerView;
            SwitchToMove();
        }

        private void OnHitPlayerListener(PlayerView playerView)
        {
            SwitchToHit();
        }

        private void SwitchToIdle()
        {
            if (_stateBehaviour.CurrentState == EnemyState.Die) return;
            
            _stateBehaviour.SwitchToState(EnemyState.Idle);
        }

        private void SwitchToMove()
        {
            if (_stateBehaviour.CurrentState == EnemyState.Die) return;
            
            _stateBehaviour.SwitchToState(EnemyState.Move);
            _movementBehaviour.InitTargetToMove(_playerView.transform);
            _animationController.PlayMovementAnimation();
            _movementBehaviour.StartMovement();
        }

        private void SwitchToHit()
        {
            if (_stateBehaviour.CurrentState == EnemyState.Die) return;
            
            _playerView.TakeDamage(_damageAmount);
            _movementBehaviour.StopMovement();

            SwitchToDie();
        }

        private void SwitchToDie()
        {
            if (_stateBehaviour.CurrentState == EnemyState.Die) return;
            _stateBehaviour.SwitchToState(EnemyState.Die);
            
            OnDeath?.Invoke(this);
            
            _deathCoroutine = StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine()
        {
            _deathParticle.Play();
            _meshObject.SetActive(false);
            yield return new WaitForSeconds(_deathDelay);
            Destroy(gameObject);
        }
    }
}