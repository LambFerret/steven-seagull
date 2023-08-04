using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SeagullBehaviour : MonoBehaviour
{
    private GameObject _head;
    private GameObject _body;
    private GameObject _leg;

    [Header("Settings")] public KeyCode keyCode;
    public float animationDuration = 0.5f;
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


    private void Awake()
    {
        _head = transform.Find("Head").gameObject;
        _body = transform.Find("Body").gameObject;
        _leg = transform.Find("Leg").gameObject;
    }

    private void Start()
    {
        _bodyOriginalScale = _body.transform.localScale;
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
        _bodyTween = _body.transform.DOScaleY(_bodyOriginalScale.y * 2, animationDuration)
            .SetEase(Ease.InOutCirc)
            .OnComplete(() =>
            {
                _body.transform.DOScaleY(_bodyOriginalScale.y, animationDuration)
                    .SetEase(Ease.InOutCirc);
            });
    }

    private bool IsAnimating()
    {
        return _bodyTween != null && _bodyTween.IsActive() && _bodyTween.IsPlaying();
    }

}