using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop.States
{
    public class TitleState : BaseState
    {
        public override void InitState()
        {
            base.InitState();

            GameRefHolder.Instance.Player.gameObject.SetActive(false);

            UIRefHolder.Instance.TitleView.ShowView();

            UIRefHolder.Instance.TitleView.OnTitleViewClicked += OnTitleViewClicked;

            GameRefHolder.Instance.AudioManager.PlayBGMusic();
        }

        public override void ExitState()
        {
            UIRefHolder.Instance.TitleView.OnTitleViewClicked -= OnTitleViewClicked;

            UIRefHolder.Instance.TitleView.HideView();

            base.ExitState();
        }


        private void OnTitleViewClicked()
        {
            StateMachine.ChangeState(new GameState());
        }
    }
}
