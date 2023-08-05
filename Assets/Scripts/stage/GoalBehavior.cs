using System;
using player;
using UnityEngine;

namespace stage
{
    public class GoalBehavior : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ball"))
            {
                EventManager.Instance.ScoreChanged(1);
                Destroy(other.gameObject);
            }
        }
    }
}