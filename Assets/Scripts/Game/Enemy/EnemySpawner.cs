using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game.Enemy
{
    public interface IEnemySpawner
    {
        public List<EnemyController> SpawnEnemies();
    }
    
    public class EnemySpawner : IEnemySpawner
    {
        private readonly ResourceProvider _resourceProvider;
        private readonly MapHelper _mapHelper;

        public EnemySpawner(ResourceProvider resourceProvider, MapHelper mapHelper)
        {
            _resourceProvider = resourceProvider;
            _mapHelper = mapHelper;
        }
        
        public List<EnemyController> SpawnEnemies()
        {
            var spawnPoints = _mapHelper.EnemySpawnZone.GetSpawnPositions();
            var enemies = new List<EnemyController>();

            foreach (var spawnPoint in spawnPoints)
            {
                var enemy = SpawnEnemy(spawnPoint, _mapHelper.EnemySpawnZone.transform);
                enemies.Add(enemy);
            }
            
            return enemies;
        }
        
        private EnemyController SpawnEnemy(Vector3 position, Transform parent)
        {
            var prefab =
                _resourceProvider.GetPrefab("Enemy"); // in a real project the key would be part of some descriptor
            
            var enemyObject = Object.Instantiate(prefab, position, Quaternion.identity);
            
            if (enemyObject.TryGetComponent(out EnemyController enemyController))
            {
                return enemyController;
            }

            throw new System.Exception("Could not find Enemy");
        }
    }
}