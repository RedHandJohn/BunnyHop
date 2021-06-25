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
        public ObjectPool DestroyablePlatformPool;

        [Header("Moving Platforms - Horizontal")]
        public ObjectPool MovingHorizontalPool;

        [Header("Moving Platforms - Vertical")]
        public ObjectPool MovingVerticalPool;

        [Header("Items")]
        public ObjectPool JetPacksPool;
        public Vector3 JetPackSpawnOffset;

        [Header("Position Parameters")]
        public Vector3 FirstPlatformPosition;
        public float XMax;
        public float YMin;
        public float YMax;

        public Vector3 PlayerStartPos;

        private float _lastXPos;
        private float _lastYPos;
        private Vector3 _newPlatformPos;
        private float _totalWeight;
        private float _random;

        public void SpawnStartingPlatforms()
        {
            _lastYPos = 0f;
            _totalWeight = StandardPlatformPool.Weight + DestroyablePlatformPool.Weight + MovingHorizontalPool.Weight + MovingVerticalPool.Weight;

            ResetAllPlatforms();

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
            _random = Random.Range(0f, _totalWeight);

            if(_random < MovingVerticalPool.Weight)
            {
                newPlatform = MovingVerticalPool.GetPooledObject();
                newPlatform.GetComponent<MovingPlatform>().Init(_lastYPos - (YMax / 2), _lastYPos + (YMax / 2));
                // give some space to next platforms
                _lastYPos += YMax/2;
            }
            else
            {
                _random -= MovingVerticalPool.Weight;
                if(_random < MovingHorizontalPool.Weight)
                {
                    newPlatform = MovingHorizontalPool.GetPooledObject();
                    newPlatform.GetComponent<MovingPlatform>().Init(_lastXPos - (XMax / 2), _lastXPos + (XMax / 2));
                }
                else
                {
                    _random -= MovingHorizontalPool.Weight;
                    if (_random < DestroyablePlatformPool.Weight)
                    {
                        newPlatform = DestroyablePlatformPool.GetPooledObject();
                    }
                    else
                    {
                        newPlatform = StandardPlatformPool.GetPooledObject();
                        SpawnJetPack(_newPlatformPos + JetPackSpawnOffset);
                    }
                }
            }

            newPlatform.GetComponent<BasePlatform>().Reset();
            newPlatform.transform.position = _newPlatformPos;
            return newPlatform;
        }

        public void ResetAllPlatforms()
        {
            StandardPlatformPool.InitPool();
            DestroyablePlatformPool.InitPool();
            MovingHorizontalPool.InitPool();
            MovingVerticalPool.InitPool();
            JetPacksPool.InitPool();
        }

        private void SpawnFirstPlatform()
        {
            var firstPlatform = StandardPlatformPool.GetPooledObject();
            firstPlatform.transform.position = FirstPlatformPosition;
        }

        private void SpawnJetPack(Vector3 position)
        {
            if(Random.Range(0f, 1f) < JetPacksPool.Weight)
            {
                var jetPack = JetPacksPool.GetPooledObject();
                jetPack.transform.position = position;
                jetPack.gameObject.SetActive(true);
            }
        }
    }
}
