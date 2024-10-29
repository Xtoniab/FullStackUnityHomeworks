using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialSize = 10;
        
        private Transform container;
        private readonly Queue<GameObject> pooledObjects = new();
        
        private void Awake()
        {
            CreateContainer();
            WarmupPool();
        }

        private void CreateContainer()
        {
            var containerObj = new GameObject($"Container");
            this.container = containerObj.transform;
            this.container.SetParent(this.transform);
            containerObj.SetActive(false);
        }

        private void WarmupPool()
        {
            for (var i = 0; i < initialSize; i++)
            {
                InstantiateAndPoolNewObject();
            }
        }

        private void InstantiateAndPoolNewObject()
        {
            var newObject = Instantiate(this.prefab, this.container);
            this.pooledObjects.Enqueue(newObject);
        }

        public GameObject Get()
        {
            if (this.pooledObjects.Count == 0)
            {
                InstantiateAndPoolNewObject();
            }

            var obj = this.pooledObjects.Dequeue();
            return obj;
        }

        public void Pool(GameObject obj)
        {
            obj.transform.SetParent(this.container);
            this.pooledObjects.Enqueue(obj);
            
        }
    }
}