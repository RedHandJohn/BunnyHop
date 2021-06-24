using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BunnyHop
{
    public class PlatformsCleaner : MonoBehaviour
    {
        public UnityAction<Collider2D> OnPlatformCollision;
        public UnityAction<Collider2D> OnPlayerCollision;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Platform"))
            {
                OnPlatformCollision?.Invoke(collision);
            }
            else if (collision.CompareTag("Player"))
            {
                OnPlayerCollision?.Invoke(collision);
            }
        }
    }
}
