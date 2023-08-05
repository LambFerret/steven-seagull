using System;
using player;
using UnityEngine;

namespace stage
{
    public class GoalBehavior : MonoBehaviour
    {
        public BallReceiver ballReceiver;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ball"))
            {
                ballReceiver.ThisBallIsCorrect(other.GetComponent<Ball>().word);
                EventManager.Instance.ScoreChanged(1);
                other.gameObject.SetActive(false);
            }
        }
    }
}