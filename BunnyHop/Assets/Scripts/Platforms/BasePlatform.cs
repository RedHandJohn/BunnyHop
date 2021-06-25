using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop.Platforms
{
    public class BasePlatform : MonoBehaviour
    {
        public virtual void Reset()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnPlayerCollision()
        {
            // do nothing
        }
    }
}
