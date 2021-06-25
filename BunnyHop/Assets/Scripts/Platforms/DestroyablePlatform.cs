using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop.Platforms
{
    public class DestroyablePlatform : BasePlatform
    {
        private Animator _animator;
        private EdgeCollider2D _collider;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<EdgeCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public override void Reset()
        {
            base.Reset();

            _animator.SetTrigger("Idle");
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public override void OnPlayerCollision()
        {
            _animator.SetTrigger("Destroy");
            _rigidbody.constraints -= RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
