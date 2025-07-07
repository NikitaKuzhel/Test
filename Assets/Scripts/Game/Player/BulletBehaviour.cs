using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Player
{
    public class BulletBehaviour : MonoBehaviour
    {
        public event Action<BulletBehaviour> OnHit;

        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        
        private PoolManager<BulletBehaviour> _poolManager;
        private Coroutine _lifetimeCoroutine;
        
        public void InitPoolManager(PoolManager<BulletBehaviour> poolManager)
        {
            _poolManager = poolManager;
        }

        public void SetPositionAndRotation(Transform spawnPoint)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;

            _lifetimeCoroutine = StartCoroutine(LifeTimeCoroutine());
        }
        
        private void Update()
        {
            transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyController enemyController))
            {
                OnHit?.Invoke(this);
                enemyController.Die();
                if (_lifetimeCoroutine != null) StopCoroutine(_lifetimeCoroutine);
                _poolManager.Return(this);
            }
        }

        private IEnumerator LifeTimeCoroutine()
        {
            yield return new WaitForSeconds(_lifeTime);
            
            _poolManager.Return(this);
        }
    }
}