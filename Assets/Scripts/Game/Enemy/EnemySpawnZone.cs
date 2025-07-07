using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemySpawnZone : MonoBehaviour
    {
        [SerializeField] private BoxCollider _spawnArea;
        [SerializeField] private float _minDistanceBetweenEnemies = 10f;
        [SerializeField] private int _maxAttemptsPerEnemy = 100;
        [SerializeField] private int _enemiesCount = 100;

        public List<Vector3> GetSpawnPositions()
        {
            List<Vector3> spawnPositions = new List<Vector3>();

            for (int i = 0; i < _enemiesCount; i++)
            {
                bool foundValidPosition = false;

                for (int attempt = 0; attempt < _maxAttemptsPerEnemy; attempt++)
                {
                    Vector3 candidate = GetRandomPointInBounds();

                    if (IsFarEnough(candidate, spawnPositions))
                    {
                        spawnPositions.Add(candidate);
                        foundValidPosition = true;
                        break;
                    }
                }

                if (!foundValidPosition)
                {
                    Debug.LogWarning(
                        $"Could not find valid spawn position for enemy {i + 1} after {_maxAttemptsPerEnemy} attempts.");
                }
            }

            return spawnPositions;
        }

        private Vector3 GetRandomPointInBounds()
        {
            Vector3 localPoint = new Vector3(
                Random.Range(-0.5f, 0.5f),
                -0.5f, // offset from collider center
                Random.Range(-0.5f, 0.5f)
            );

            localPoint = Vector3.Scale(localPoint, _spawnArea.size);

            return _spawnArea.transform.TransformPoint(_spawnArea.center + localPoint);
        }

        private bool IsFarEnough(Vector3 point, List<Vector3> existingPoints)
        {
            foreach (Vector3 existing in existingPoints)
            {
                if (Vector3.Distance(point, existing) < _minDistanceBetweenEnemies)
                    return false;
            }

            return true;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            if (_spawnArea == null) _spawnArea = GetComponent<BoxCollider>();
            Gizmos.DrawWireCube(_spawnArea.center + transform.position, _spawnArea.size);
        }
#endif
    }
}