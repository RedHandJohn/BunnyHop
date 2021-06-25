using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace BunnyHop.Views
{
    public class GameView : BaseView
    {
        public Animator CountdownAnimator;
        public float CountdownAnimationLength;

        public GameObject TopBar;
        public TextMeshProUGUI PlatformsCountText;
        public TextMeshProUGUI ScoreCountText;

        public UnityAction OnCountdownAnimationEnd;


        public void StartIntroAnimation()
        {
            StartCoroutine(CountDownCoroutine());
        }

        private IEnumerator CountDownCoroutine()
        {
            CountdownAnimator.SetTrigger("Counting");

            yield return new WaitForSeconds(CountdownAnimationLength);

            CountdownAnimator.SetTrigger("Idle");
            OnCountdownAnimationEnd?.Invoke();
        }
    }
}
