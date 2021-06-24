using BunnyHop.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop.States
{
    public class GameState : BaseState
    {
        private float _horizontalInput;
        private bool _playerIsFalling;

        public override void InitState()
        {
            base.InitState();

            GameRefHolder.Instance.Player.gameObject.SetActive(true);
            GameRefHolder.Instance.Player.OnPlayerCollisionEnter += OnPlayerCollisionEnter;
            GameRefHolder.Instance.PlatformsCleaner.OnPlatformCollision += OnCleanerPlatformCollision;
            GameRefHolder.Instance.PlatformsCleaner.OnPlayerCollision += OnCleanerPlayerCollision;

            ResetGameObjects();
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();

            _horizontalInput = Input.GetAxis("Horizontal");
            if(_horizontalInput != 0)
            {
                GameRefHolder.Instance.Player.UpdateHorizontalMovement(_horizontalInput);
            }

            GameRefHolder.Instance.Player.CheckIsFalling();
        }

        public override void ExitState()
        {
            GameRefHolder.Instance.Player.OnPlayerCollisionEnter -= OnPlayerCollisionEnter;
            GameRefHolder.Instance.PlatformsCleaner.OnPlatformCollision -= OnCleanerPlatformCollision;
            GameRefHolder.Instance.PlatformsCleaner.OnPlayerCollision -= OnCleanerPlayerCollision;

            base.ExitState();
        }

        private void ResetGameObjects()
        {
            GameRefHolder.Instance.Player.Reset(GameRefHolder.Instance.LevelManager.PlayerStartPos);
            GameRefHolder.Instance.TrackingCamera.ResetPosition();
            GameRefHolder.Instance.LevelManager.SpawnStartingPlatforms();
        }

        private void OnCleanerPlatformCollision(Collider2D platformCollider)
        {
            GameObject.Destroy(platformCollider.gameObject);
            GameRefHolder.Instance.LevelManager.GetNewPlatform();
        }

        private void OnCleanerPlayerCollision(Collider2D playerCollider)
        {
            PlayerDeath();
        }

        private void PlayerDeath()
        {
            GameRefHolder.Instance.Player.Die();
            StateMachine.ChangeState(new TitleState());
        }


        private void OnPlayerCollisionEnter(Collision2D collision)
        {
            if (GameRefHolder.Instance.Player.IsFalling && collision.collider.CompareTag("Platform"))
            {
                GameRefHolder.Instance.Player.Bounce();
                collision.collider.GetComponent<DestroyablePlatform>()?.OnPlayerCollision();
            }
        }
    }
}
