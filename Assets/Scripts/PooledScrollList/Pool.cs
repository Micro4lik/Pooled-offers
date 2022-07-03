using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PooledScrollList
{
    public class Pool<T> : IDisposable where T : Component
    {
        private T itemPrefab;
        private Transform parentObject;
        private Transform poolRoot;
        private Queue<T> itemsQueue;

        public int Count => itemsQueue.Count;

        public Pool(T itemPrefab, Transform parentObject, int poolCapacity)
        {
            this.itemPrefab = itemPrefab;
            this.parentObject = parentObject;
            itemsQueue = new Queue<T>(poolCapacity);

            poolRoot = new GameObject($"[{itemPrefab.GetType().Name}] Pool").transform;
            poolRoot.SetParent(parentObject, false);

            for (var i = 0; i < poolCapacity; i++)
            {
                var item = Object.Instantiate(itemPrefab, poolRoot);
                item.gameObject.SetActive(false);
                itemsQueue.Enqueue(item);
            }
        }

        public T GetNext()
        {
            T item;

            if (itemsQueue.Count > 0)
            {
                item = itemsQueue.Dequeue();
                item.transform.SetParent(parentObject, false);
                item.gameObject.SetActive(true);
            }
            else
            {
                item = Object.Instantiate(itemPrefab, parentObject);
            }

            return item;
        }

        public void Return(T item)
        {
            item.gameObject.SetActive(false);
            item.transform.localPosition = Vector3.zero;
            item.transform.localEulerAngles = Vector3.zero;
            item.transform.SetParent(poolRoot, false);

            itemsQueue.Enqueue(item);
        }

        public void Dispose()
        {
            foreach (var item in itemsQueue)
            {
                Object.Destroy(item.gameObject);
            }

            Object.Destroy(poolRoot.gameObject);

            itemPrefab = null;
            parentObject = null;
            poolRoot = null;
            itemsQueue = null;
        }
    }
}