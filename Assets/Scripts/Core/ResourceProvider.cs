using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Core
{

    [CreateAssetMenu(fileName = "ResourceProvider", menuName = "Create/Resource Provider")]
    public class ResourceProvider : ScriptableObject
    {
        private const string PrefabResourcePathFormat = "Prefabs/";

        [SerializeField] private List<ResourceDescriptor> _resources = new();
        
        public GameObject GetPrefab(string nameId)
        {
            var descriptor = GetResourceDescriptor(nameId);
            if (descriptor != null)
            {
                var path = $"{PrefabResourcePathFormat}{descriptor.Path}";
                var result = UnityEngine.Resources.Load<GameObject>(path);
                return result;
            }
            Debug.LogError($"[{GetType().Name}][GetPrefab]Can't find prefab with id: {nameId}");
            return null;
        }

        private ResourceDescriptor GetResourceDescriptor(string nameId)
        {
            var result = _resources.Find(resource => resource.Id == nameId);
            return result;
        }
    }

    [System.Serializable]
    public class ResourceDescriptor
    {
        [SerializeField] private string _id;
        [SerializeField] private string _path;

        public string Id => _id;
        public string Path => _path;

        public ResourceDescriptor(string id, string path)
        {
            _id = id;
            _path = path;
        }
    }
}