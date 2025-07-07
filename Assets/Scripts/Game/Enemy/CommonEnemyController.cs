using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public interface ICommonEnemyController
    {
        void SpawnEnemies();
        void DestroyEnemies();
    }
    
    public class CommonEnemyController : ICommonEnemyController
    {
        private readonly IEnemySpawner _enemySpawner;
        private List<EnemyController> _enemies = new();

        public CommonEnemyController(IEnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public void SpawnEnemies()
        {
            _enemies = _enemySpawner.SpawnEnemies();

            foreach (var enemyController in _enemies)
            {
                enemyController.OnDeath += OnEnemyDeath;
                enemyController.Subscribe();
                enemyController.Activate();
            }
        }

        public void DestroyEnemies()
        {
            foreach (var enemyController in _enemies)
            {
                enemyController.OnDeath -= OnEnemyDeath;
                enemyController.Unsubscribe();
                UnityEngine.Object.Destroy(enemyController.gameObject);
            }
            
            _enemies.Clear();
        }

        private void OnEnemyDeath(EnemyController enemyController)
        {
            enemyController.OnDeath -= OnEnemyDeath;
            enemyController.Unsubscribe();
            _enemies.Remove(enemyController);
        }
    }
}