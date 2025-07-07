using Game.Player;
using UnityEngine;

namespace Game
{
    public class PoolInitializer : MonoBehaviour
    {
        [SerializeField] private BulletBehaviour _bulletPrefab;
        [SerializeField] private Transform _parent;

        
        public PoolManager<BulletBehaviour> BulletPool { get; private set;}
        
        public void InitializePools()
        {
            BulletPool = new PoolManager<BulletBehaviour>(_bulletPrefab, _parent);

        }

        public void ClearPools()
        {
            BulletPool.Release();
        }
    }
}