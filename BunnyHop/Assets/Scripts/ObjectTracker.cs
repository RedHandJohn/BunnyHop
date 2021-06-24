using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop
{
    public class ObjectTracker : MonoBehaviour
    {
        public GameObject TrackedObject;
        public bool NegativeYTrackingEnabled;
        public bool LerpEnabled;
        public float LerpSpeed;

        private float _initialDistance;
        private Vector3 _newPosition;
        private float _distance;

        private void Start()
        {
            _initialDistance = transform.position.y - TrackedObject.transform.position.y;
        }

        private void FixedUpdate()
        {
            _distance = TrackedObject.transform.position.y - transform.position.y;
            if (_distance > 0 || NegativeYTrackingEnabled)
            {
                _newPosition = new Vector3(transform.position.x, TrackedObject.transform.position.y + _initialDistance, transform.position.z);
                if (LerpEnabled)
                {
                    transform.position = Vector3.Lerp(transform.position, _newPosition, LerpSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    transform.position = _newPosition;
                }

            }
        }

        public void ResetPosition()
        {
            transform.position = new Vector3(transform.position.x, TrackedObject.transform.position.y + _initialDistance, transform.position.z);
        }
    }
}
