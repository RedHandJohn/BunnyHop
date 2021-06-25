using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BunnyHop
{
    public class PlayerController : MonoBehaviour
    {
        public float JumpSpeed;
        public float HorizontalSpeed;
        public UnityAction<Collision2D> OnPlayerCollisionEnter;

        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private bool _isFalling;
        public bool IsFalling { get { return _isFalling; } }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnPlayerCollisionEnter?.Invoke(collision);
        }

        public void Bounce()
        {
            Vector2 jumpVelocity = new Vector2(0f, JumpSpeed);
            _rigidbody.velocity += jumpVelocity;
            _animator.SetTrigger("Bounce");
            _isFalling = false;
        }

        public void Reset(Vector3 startPos)
        {
            transform.position = startPos;
            _rigidbody.velocity = Vector3.zero;
        }

        public void UpdateHorizontalMovement(float horizontalMove)
        {
            _rigidbody.velocity = new Vector2(horizontalMove * HorizontalSpeed, _rigidbody.velocity.y);
        }

        public void CheckIsFalling()
        {
            if (_rigidbody.velocity.y <= 0 && !_isFalling)
            {
                _isFalling = true;
                _animator.SetTrigger("Fall");
            }
        }
    }
}
