using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PoolManager<T> where T : MonoBehaviour
    {
        private T _prefab;
        private Transform _parent;
    
        private List<T> _pool = new List<T>();
        private List<T> _active = new List<T>();

        public PoolManager(T prefab, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public void Release()
        {
            while (_pool.Count > 0)
            {
                var element = _pool[0];
                _pool.Remove(element);
                //destroy all elements and unsubscribe
                Object.Destroy(element.gameObject);
            }
        
            while (_active.Count > 0)
            {
                var element = _active[0];
                _active.Remove(element);
                //destroy all elements and unsubscribe
                Object.Destroy(element.gameObject);
            }
        }

        public bool Return(T prefab)
        {
            if(_active.Contains(prefab) == false) return false;
            _pool.Add(prefab);
            _active.Remove(prefab);
            prefab.gameObject.SetActive(false);
            return true;
        }

        public T Get()
        {
            T result = null;
            if (_pool.Count == 0)
            {
                result = Object.Instantiate(_prefab, _parent);
                //any subscribtion
            }
            else
            {
                result = _pool[0];
                _pool.RemoveAt(0);
                result.gameObject.SetActive(true);
            }
            _active.Add(result);
            return result;
        }
    }
}