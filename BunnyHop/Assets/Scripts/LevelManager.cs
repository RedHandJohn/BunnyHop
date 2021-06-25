using BunnyHop.Platforms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop
{
    public class LevelManager : MonoBehaviour
    {
        public int InitialPlatformCount;

        [Header("Standard Platforms")]
        public ObjectPool StandardPlatformPool;

        [Header("Destroyable Platforms")]
        [Range(0f, 1f)]
        public float DestroyableProbability;
        public ObjectPool DestroyablePlatformPool;

        [Header("Position Parameters")]
        public Vector3 FirstPlatformPosition;
        public float XMax;
        public float YMin;
        public float YMax;

        public Vector3 PlayerStartPos;

        private float _lastXPos;
        private float _lastYPos;
        private Vector3 _newPlatformPos;

        public void SpawnStartingPlatforms()
        {
            StandardPlatformPool.InitPool();
            DestroyablePlatformPool.InitPool();
            ResetAllPlatforms();

            _lastYPos = 0f;

            SpawnFirstPlatform();

            for (int i = 0; i < InitialPlatformCount; i++)
            {
                GetNewPlatform();
            }
        }

        public GameObject GetNewPlatform()
        {
            _lastXPos = Random.Range(-XMax, XMax);
            _lastYPos = Random.Range(_lastYPos + YMin, _lastYPos + YMax);
            _newPlatformPos = new Vector3(_lastXPos, _lastYPos, 0f);

            GameObject newPlatform;
            if (Random.Range(0f, 1f) < DestroyableProbability)
            {
                newPlatform = DestroyablePlatformPool.GetPooledObject();

            }
            else
            {
                newPlatform = StandardPlatformPool.GetPooledObject();
            }

            newPlatform.GetComponent<BasePlatform>().Reset();
            newPlatform.transform.position = _newPlatformPos;
            return newPlatform;
        }

        public void ResetAllPlatforms()
        {
            foreach (Transform child in StandardPlatformPool.ParentTransform)
            {
                child.gameObject.SetActive(false);
            }

            foreach (Transform child in DestroyablePlatformPool.ParentTransform)
            {
                child.gameObject.SetActive(false);
            }
        }

        private void SpawnFirstPlatform()
        {
            var firstPlatform = StandardPlatformPool.GetPooledObject();
            firstPlatform.transform.position = FirstPlatformPosition;
        }
    }
}
