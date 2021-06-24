using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BunnyHop
{
    public class InputManager : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float MinDragDistanceMultipler;
        public float HorizontalDragMultiplier;

        public UnityAction<float> OnHorizontalInput;

        // touch
        private Vector2 _firstTouch;
        private Vector2 _lastTouch;
        private float _minDragDistance;
        private float _dragLength;
        private bool _dragEnded;
        // keys
        private float _horizontalAxisValue;

        private void Start()
        {
            _minDragDistance = Screen.width * MinDragDistanceMultipler;
        }

        public void CheckInputHorizontal()
        {
            CheckHorizontalAxis();
#if !UNITY_EDITOR
            CheckHorizontalDrag();
#endif
        }

        private void CheckHorizontalAxis()
        {
            _horizontalAxisValue = Input.GetAxis("Horizontal");
            if (_horizontalAxisValue != 0)
            {
                OnHorizontalInput?.Invoke(_horizontalAxisValue);
            }
        }

#if !UNITY_EDITOR
        private void CheckHorizontalDrag()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _firstTouch = touch.position;
                    _lastTouch = touch.position;
                    _dragEnded = false;
                }
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)
                {
                    _lastTouch = touch.position;
                    HandleDrag();
                }
            }
        }

        private void HandleDrag()
        {
            if(!_dragEnded)
            {
                _dragLength = _lastTouch.x - _firstTouch.x;
                if (Mathf.Abs(_dragLength) > _minDragDistance)
                {
                    OnHorizontalInput?.Invoke(_dragLength / Screen.width * HorizontalDragMultiplier);
                    _dragEnded = true;
                }
            }
        }
#endif
    }
}
