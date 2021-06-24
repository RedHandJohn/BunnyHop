using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BunnyHop.Views
{
    public class TitleView : BaseView
    {
        public UnityAction OnTitleViewClicked;

        public void TitleViewClick()
        {
            OnTitleViewClicked?.Invoke();
        }
    }
}
