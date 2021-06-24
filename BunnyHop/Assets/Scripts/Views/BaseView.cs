using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        public GameObject ChildrenContainer;

        [ContextMenu("ShowView")]
        public virtual void ShowView()
        {
            ChildrenContainer.SetActive(true);
        }

        [ContextMenu("HideView")]
        public virtual void HideView()
        {
            ChildrenContainer.SetActive(false);
        }
    }
}
