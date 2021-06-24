using BunnyHop.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop.States
{
    public class GameState : BaseState
    {
        public override void InitState()
        {
            base.InitState();

            GameRefHolder.Instance.Player.gameObject.SetActive(true);

            GameRefHolder.Instance.Player.OnPlayerCollisionEnter += OnPlayerCollisionEnter;
            GameRefHolder.Instance.PlatformsCleaner.OnPlatformCollision += OnCleanerPlatformCollision;
            GameRefHolder.Instance.PlatformsCleaner.OnPlayerCollision += OnCleanerPlayerCollision;
            GameRefHolder.Instance.InputManager.OnHorizontalInput += OnInputHorizontal;

            ResetGameObjects();
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();

            GameRefHolder.Instance.InputManager.CheckInputHorizontal();
            GameRefHolder.Instance.Player.CheckIsFalling();
        }

        public override void ExitState()
        {
            GameRefHolder.Instance.Player.OnPlayerCollisionEnter -= OnPlayerCollisionEnter;
            GameRefHolder.Instance.PlatformsCleaner.OnPlatformCollision -= OnCleanerPlatformCollision;
            GameRefHolder.Instance.PlatformsCleaner.OnPlayerCollision -= OnCleanerPlayerCollision;
            GameRefHolder.Instance.InputManager.OnHorizontalInput -= OnInputHorizontal;

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

        private void OnInputHorizontal(float horizontalInput)
        {
            GameRefHolder.Instance.Player.UpdateHorizontalMovement(horizontalInput);
        }
    }
}
