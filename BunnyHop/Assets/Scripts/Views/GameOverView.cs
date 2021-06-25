using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BunnyHop.Views
{
    public class GameOverView : BaseView
    {
        public Animator TextsAnimator;
        public float TextsAnimationDuration;

        public Button ContinueButton;

        public TextMeshProUGUI YourScoreText;
        public TextMeshProUGUI HighScoreText;

        public UnityAction OnContinueButtonClicked;
        public UnityAction OnTextsAnimationFinished;

        public void ContinueButtonClick()
        {
            OnContinueButtonClicked?.Invoke();
        }

        public void StartTextsAnimation()
        {
            StartCoroutine(TextsAnimationCoroutine());
        }

        private IEnumerator TextsAnimationCoroutine()
        {
            TextsAnimator.SetTrigger("Slide");

            yield return new WaitForSeconds(TextsAnimationDuration);

            TextsAnimator.SetTrigger("Idle");
            OnTextsAnimationFinished?.Invoke();
        }
    }
}
