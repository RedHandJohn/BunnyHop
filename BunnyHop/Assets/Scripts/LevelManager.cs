using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop
{
    public class LevelManager : MonoBehaviour
    {
        public Transform PlatformsParent;
        public GameObject StandardPlatformPrefab;
        public GameObject DestroyablePlatformPrefab;
        [Range(0f, 1f)]
        public float DestroyableProbability;

        public Vector3 FirstPlatformPosition;
        public int StartingPlatforms;
        public float XMax;
        public float YMin;
        public float YMax;

        public Vector3 PlayerStartPos;

        private float _lastXPos;
        private float _lastYPos;
        private Vector3 _newPlatformPos;

        public void SpawnStartingPlatforms()
        {
            DestroyAllPlatforms();

            _lastYPos = 0f;

            GameObject.Instantiate(StandardPlatformPrefab, FirstPlatformPosition, Quaternion.identity, PlatformsParent);

            for (int i = 0; i < StartingPlatforms - 1; i++)
            {
                GetNewPlatform();
            }
        }

        public GameObject GetNewPlatform()
        {
            _lastXPos = Random.Range(-XMax, XMax);
            _lastYPos = Random.Range(_lastYPos + YMin, _lastYPos + YMax);
            _newPlatformPos = new Vector3(_lastXPos, _lastYPos, 0f);

            if (Random.Range(0f, 1f) < DestroyableProbability)
            {
                return GameObject.Instantiate(DestroyablePlatformPrefab, _newPlatformPos, Quaternion.identity, PlatformsParent);
            }
            else
            {

                return GameObject.Instantiate(StandardPlatformPrefab, _newPlatformPos, Quaternion.identity, PlatformsParent);
            }
        }

        public void DestroyAllPlatforms()
        {
            foreach (Transform child in PlatformsParent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
