using BunnyHop.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop
{
    public class GameRefHolder : SingletonMonoBehaviour<GameRefHolder>
    {
        public PlayerController Player;
        public LevelManager LevelManager;
        public PlatformsCleaner PlatformsCleaner;
        public ObjectTracker TrackingCamera;
        public InputManager InputManager;
    }
}
