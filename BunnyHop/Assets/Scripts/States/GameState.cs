using BunnyHop.Platforms;
using BunnyHop.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop.States
{
    public class GameState : BaseState
    {
        private bool _inputEnabled;
        private int _jumpedPlatformsCount;
        private int _currentScore;

        private float _jetPackTimeLeft;

        public override void InitState()
        {
            base.InitState();

            GameRefHolder.Instance.Player.OnPlayerCollisionEnter += OnPlayerCollisionEnter;
            GameRefHolder.Instance.PlatformsCleaner.OnPlatformCollision += OnCleanerPlatformCollision;
            GameRefHolder.Instance.PlatformsCleaner.OnPlayerCollision += OnCleanerPlayerCollision;
            GameRefHolder.Instance.InputManager.OnHorizontalInput += OnInputHorizontal;

            UIRefHolder.Instance.GameView.OnCountdownAnimationEnd += OnIntroCountdownEnd;

            GameRefHolder.Instance.Player.gameObject.SetActive(true);
            GameRefHolder.Instance.Player.Reset(GameRefHolder.Instance.LevelManager.PlayerStartPos);
            GameRefHolder.Instance.TrackingCamera.ResetPosition();
            GameRefHolder.Instance.LevelManager.SpawnStartingPlatforms();

            UIRefHolder.Instance.GameView.ShowView();
            UIRefHolder.Instance.GameView.StartIntroAnimation();
            UIRefHolder.Instance.GameView.TopBar.SetActive(false);
            GameRefHolder.Instance.Player.gameObject.SetActive(false);
            _inputEnabled = false;
            _jumpedPlatformsCount = 0;
            _currentScore = 0;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            UpdateScore();
            UpdateJetPackTime();
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();

            if(_inputEnabled)
            {
                GameRefHolder.Instance.InputManager.CheckInputHorizontal();
                GameRefHolder.Instance.Player.CheckIsFalling();
                JetPackPlayer();
            }
        }

        public override void ExitState()
        {
            GameRefHolder.Instance.Player.OnPlayerCollisionEnter -= OnPlayerCollisionEnter;
            GameRefHolder.Instance.PlatformsCleaner.OnPlatformCollision -= OnCleanerPlatformCollision;
            GameRefHolder.Instance.PlatformsCleaner.OnPlayerCollision -= OnCleanerPlayerCollision;
            GameRefHolder.Instance.InputManager.OnHorizontalInput -= OnInputHorizontal;

            UIRefHolder.Instance.GameView.OnCountdownAnimationEnd -= OnIntroCountdownEnd;

            UIRefHolder.Instance.GameView.HideView();

            base.ExitState();
        }

        private void OnCleanerPlatformCollision(Collider2D platformCollider)
        {
            platformCollider.gameObject.SetActive(false);
            GameRefHolder.Instance.LevelManager.GetNewPlatform();
        }

        private void OnCleanerPlayerCollision(Collider2D playerCollider)
        {
            PlayerDeath();
        }

        private void PlayerDeath()
        {
            GameRefHolder.Instance.Player.gameObject.SetActive(false);
            StateMachine.ChangeState(new GameOverState(_currentScore));
        }


        private void OnPlayerCollisionEnter(Collision2D collision)
        {
            if (GameRefHolder.Instance.Player.IsFalling && collision.collider.CompareTag("Platform"))
            {
                GameRefHolder.Instance.Player.Bounce();
                collision.collider.GetComponent<BasePlatform>()?.OnPlayerCollision();
                _jumpedPlatformsCount++;
                UIRefHolder.Instance.GameView.PlatformsCountText.text = _jumpedPlatformsCount.ToString();
                UpdateScore();
            }
            else if(collision.collider.CompareTag("JetPack"))
            {
                collision.gameObject.SetActive(false);

                GameRefHolder.Instance.Player.StartJetPack();
                _jetPackTimeLeft = GameRefHolder.Instance.Player.JetPackDuration;
            }
        }

        private void OnInputHorizontal(float horizontalInput)
        {
            GameRefHolder.Instance.Player.UpdateHorizontalMovement(horizontalInput);
        }

        private void OnIntroCountdownEnd()
        {
            _inputEnabled = true;
            GameRefHolder.Instance.Player.gameObject.SetActive(true);
            UIRefHolder.Instance.GameView.TopBar.SetActive(true);
            UIRefHolder.Instance.GameView.PlatformsCountText.text = _jumpedPlatformsCount.ToString();
            UIRefHolder.Instance.GameView.ScoreCountText.text = _currentScore.ToString();
        }

        private void UpdateScore()
        {
            _currentScore = Mathf.RoundToInt(GameRefHolder.Instance.Player.transform.position.y * 10);
            UIRefHolder.Instance.GameView.ScoreCountText.text = _currentScore.ToString();
        }

        private void UpdateJetPackTime()
        {
            if(GameRefHolder.Instance.Player.IsJetpacking)
            {
                if (_jetPackTimeLeft > 0)
                {
                    _jetPackTimeLeft -= Time.deltaTime;
                }
                else
                {
                    GameRefHolder.Instance.Player.StopJetPack();
                }
            }
        }

        private void JetPackPlayer()
        {
            if (GameRefHolder.Instance.Player.IsJetpacking)
            {
                GameRefHolder.Instance.Player.MaintainJetPack();
            }
        }
    }
}
