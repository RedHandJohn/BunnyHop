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
        public float JetPackSpeed;
        public float JetPackDuration;

        public Animator JetPackAnimator;

        public UnityAction<Collision2D> OnPlayerCollisionEnter;

        private Rigidbody2D _rigidbody;
        private Animator _animator;

        [SerializeField]
        private bool _isFalling;
        public bool IsFalling { get { return _isFalling; } }

        private bool _isJetpacking;
        public bool IsJetpacking { get { return _isJetpacking; } }

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

        public void StartJetPack()
        {
            _animator.SetBool("IsJetPacking", true);
            _isFalling = false;
            _isJetpacking = true;
        }

        public void MaintainJetPack()
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JetPackSpeed);
        }

        public void StopJetPack()
        {
            _isJetpacking = false;
            _animator.SetBool("IsJetPacking", false);
        }
    }
}
