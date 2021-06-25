using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop
{
    [Serializable]
    public class ObjectPool
    {
        public Transform ParentTransform;
        public GameObject Prefab;
        public int InitialCount;

        public int CurrentCount { get { return _pool.Count; } }

        [SerializeField]
        private List<GameObject> _pool;

        public void InitPool()
        {
            if(_pool == null)
            {
                _pool = new List<GameObject>();
            }

            if(_pool.Count == 0)
            {
                for (int i = 0; i < InitialCount; i++)
                {
                    var newObject = GameObject.Instantiate(Prefab, ParentTransform);
                    _pool.Add(newObject);
                }
            }
        }

        public GameObject GetPooledObject()
        {
            for(int i = 0; i < _pool.Count; i++)
            {
                if(!_pool[i].activeInHierarchy)
                {
                    _pool[i].SetActive(true);
                    return _pool[i];
                }
            }

            var newObject = GameObject.Instantiate(Prefab, ParentTransform);
            _pool.Add(newObject);
            return newObject;
        }
    }
}
