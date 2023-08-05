using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [Tooltip("ù??° ????? ????? ????? ???")]
    public Vector2[] route;

    private int ptr;
    public float duration = 2f;

    enum obstacleState
    {
        start = 1,
        onGoing = 2,
        end = 3
    }

    obstacleState state;

    // Start is called before the first frame update
    void Start()
    {
        route[0] = transform.position;
        state = obstacleState.start;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == obstacleState.start)
        {
            NextMove();
        }
        else if (state == obstacleState.end)
        {
            state = obstacleState.start;
        }
    }

    void NextMove()
    {
        state = obstacleState.onGoing;
        transform.DOMove(route[ptr], duration)
            .SetEase(Ease.OutCirc)
            .OnComplete(() =>
            {
                state = obstacleState.end;
                if (++ptr >= route.Length)
                {
                    ptr = 0;
                }
            });
    }
}