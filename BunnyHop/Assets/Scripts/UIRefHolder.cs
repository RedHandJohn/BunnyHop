using BunnyHop.Util;
using BunnyHop.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop
{
    public class UIRefHolder : SingletonMonoBehaviour<UIRefHolder>
    {
        public TitleView TitleView;
        public GameView GameView;
        public GameOverView GameOverView;
        public GameObject BackgroundDefault;
        public GameObject BackgroundJetPack;
    }
}
