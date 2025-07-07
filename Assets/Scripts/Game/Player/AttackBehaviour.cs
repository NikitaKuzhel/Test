using System;
using System.Collections;
using Game.Enemy;
using UnityEngine;

namespace Game.Player
{
    public class AttackBehaviour : MonoBehaviour
    {
        private const float DelayTime = 0.75f;
        
        [SerializeField] private Transform _spawnPoint;
        
        private PoolManager<BulletBehaviour> _poolManager;
        private Coroutine _shootingCoroutine;
        
        public void InitPoolManager(PoolManager<BulletBehaviour> poolManager)
        {
            _poolManager = poolManager;
        }

        public void StartShooting()
        {
            _shootingCoroutine = StartCoroutine(Shooting());
        }

        public void StopShooting()
        {
            StopCoroutine(_shootingCoroutine);
        }

        private IEnumerator Shooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(DelayTime);
            
                var bullet = _poolManager.Get();
                bullet.InitPoolManager(_poolManager);
                bullet.SetPositionAndRotation(_spawnPoint);
                bullet.OnHit += OnHitListener;
            }
        }

        private void OnHitListener(BulletBehaviour bullet)
        {
            bullet.OnHit -= OnHitListener;
        }
    }
}