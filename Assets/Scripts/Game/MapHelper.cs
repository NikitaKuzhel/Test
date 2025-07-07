using Game.Enemy;
using UnityEngine;

namespace Game
{
    public class MapHelper : MonoBehaviour
    {
        [field: SerializeField] public CameraFollow CameraFollow { get; private set; }
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public WinTrigger WinTrigger { get; private set; }
        [field: SerializeField] public EnemySpawnZone EnemySpawnZone { get; private set; }
        [field: SerializeField] public PoolInitializer PoolInitializer { get; private set; }
    }
}