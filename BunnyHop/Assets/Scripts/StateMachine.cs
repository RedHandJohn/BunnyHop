using BunnyHop.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop
{
    public class StateMachine : MonoBehaviour
    {
        private BaseState _currentState;

        private void Start()
        {
            ChangeState(new TitleState());
        }

        private void Update()
        {
            _currentState?.UpdateState();
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdateState();
        }

        public void ChangeState(BaseState newState)
        {
            _currentState?.ExitState();

            _currentState = newState;

            if(_currentState != null)
            {
                _currentState.StateMachine = this;
                _currentState.InitState();
            }
        }
    }
}
