using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop
{
    public class DestroyablePlatform : MonoBehaviour
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

        public void OnPlayerCollision()
        {
            _collider.enabled = false;
            _animator.SetTrigger("Destroy");
            _rigidbody.constraints -= RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
