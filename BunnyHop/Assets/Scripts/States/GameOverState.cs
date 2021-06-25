using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop.States
{
    public class GameOverState : BaseState
    {
        private int _currentScore;
        private int _highScore;
        private const string HIGH_SCORE_KEY = "HighScore";

        public GameOverState(int currentScore)
        {
            _currentScore = currentScore;
            _highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY);
            if(_currentScore > _highScore)
            {
                _highScore = _currentScore;
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, _highScore);
            }
        }

        public override void InitState()
        {
            base.InitState();

            UIRefHolder.Instance.GameOverView.OnContinueButtonClicked += OnContinueButtonClicked;
            UIRefHolder.Instance.GameOverView.OnTextsAnimationFinished += OnTextsAnimationFinished;

            UIRefHolder.Instance.GameOverView.ShowView();
            UpdateScoreTexts();
            UIRefHolder.Instance.GameOverView.StartTextsAnimation();
            UIRefHolder.Instance.GameOverView.ContinueButton.interactable = false;
            GameRefHolder.Instance.AudioManager.PlayGameOverSound();
        }

        public override void ExitState()
        {
            UIRefHolder.Instance.GameOverView.OnContinueButtonClicked -= OnContinueButtonClicked;
            UIRefHolder.Instance.GameOverView.OnTextsAnimationFinished -= OnTextsAnimationFinished;

            UIRefHolder.Instance.GameOverView.HideView();

            base.ExitState();
        }

        private void OnContinueButtonClicked()
        {
            StateMachine.ChangeState(new GameState());
        }

        private void OnTextsAnimationFinished()
        {
            UIRefHolder.Instance.GameOverView.ContinueButton.interactable = true;
        }

        private void UpdateScoreTexts()
        {
            UIRefHolder.Instance.GameOverView.YourScoreText.text = _currentScore.ToString();
            UIRefHolder.Instance.GameOverView.HighScoreText.text = _highScore.ToString();
        }
    }
}
