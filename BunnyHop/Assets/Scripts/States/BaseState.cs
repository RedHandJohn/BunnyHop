using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop.States
{
    public abstract class BaseState
    {
        public StateMachine StateMachine;

        public virtual void InitState() { }
        public virtual void UpdateState() { }
        public virtual void FixedUpdateState() { }
        public virtual void ExitState() { }
    }
}
