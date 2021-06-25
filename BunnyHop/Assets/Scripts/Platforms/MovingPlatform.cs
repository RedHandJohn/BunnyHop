using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop.Platforms
{
    public class MovingPlatform : BasePlatform
    {
        public enum MovementAxis
        {
            Horizontal = 0,
            Vertical = 1
        }

        public float Speed;
        public MovementAxis Axis;

        private float _min;
        private float _max;
        private Vector3 _direction;

        public override void Reset()
        {
            base.Reset();
        }

        public void Init(float min, float max)
        {
            _min = min;
            _max = max;

            SetInitialDirection();
        }

        private void Update()
        {
            UpdateDirection();
            transform.position += _direction * Speed * Time.deltaTime;
        }

        private void UpdateDirection()
        {
            if(Axis == MovementAxis.Horizontal)
            {
                if (transform.position.x <= _min)
                {
                    _direction = Vector3.right;
                }
                else if (transform.position.x >= _max)
                {
                    _direction = Vector3.left;
                }
            }
            else
            {
                if (transform.position.y <= _min)
                {
                    _direction = Vector3.up;
                }
                else if (transform.position.y >= _max)
                {
                    _direction = Vector3.down;
                }
            }
        }

        private void SetInitialDirection()
        {
            if(Axis == MovementAxis.Horizontal)
            {
                _direction = Vector3.right;
            }
            else
            {
                _direction = Vector3.up;
            }
        }
    }

}