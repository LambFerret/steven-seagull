using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SeagullBehaviour : MonoBehaviour
{
    [Header("Settings")] public KeyCode keyCode;
    public float animationDuration = 0.5f;
    public float animationHeight = 2f;
    [Header("Information")] public State currentState = State.Idle;
    private Vector3 _bodyOriginalScale;
    private Tween _bodyTween;
    private bool _isAnimating;

    public enum State
    {
        Idle,
        Pressed,
        Flying
    }

    private void Start()
    {
        _bodyOriginalScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && !IsAnimating())
        {
            _bodyTween.Kill();
            StartBodyAnimation();
        }
    }

    private void StartBodyAnimation()
    {
        currentState = State.Pressed;
        _bodyTween = transform.DOMoveY(_bodyOriginalScale.y + animationHeight, animationDuration)
            .SetEase(Ease.InOutCirc)
            .OnComplete(() =>
            {
                transform.DOMoveY(_bodyOriginalScale.y, animationDuration).SetEase(Ease.InOutCirc);
            });
    }

    private bool IsAnimating()
    {
        return _bodyTween != null && _bodyTween.IsActive() && _bodyTween.IsPlaying();
    }
}