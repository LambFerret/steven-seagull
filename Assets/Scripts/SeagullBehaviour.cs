using DG.Tweening;
using UnityEngine;

public class SeagullBehaviour : MonoBehaviour
{
    [Header("Settings")] public KeyCode keyCode;
    public float animationDuration = 0.5f;
    float animationHeight = 1.1f;
    [Header("Information")] public State currentState = State.Idle;
    private Vector3 _bodyOriginalPosition;
    private Tween _bodyTween;
    private bool _isAnimating;
    private Rigidbody2D _rb;

    public enum State
    {
        Idle,
        Pressed,
        Flying
    }

    private void Start()
    {
        _bodyOriginalPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && !IsAnimating())
        {
            _bodyTween.Kill();
            // StartBodyAnimation();
            StartRigidAnimation();
        }
    }

    private void StartRigidAnimation()
    {
        currentState = State.Pressed;
        _bodyTween = _rb.DOMoveY(_bodyOriginalPosition.y + animationHeight, animationDuration)
            .SetEase(Ease.InOutCirc)
            .OnComplete(() =>
            {
                transform.DOMoveY(_bodyOriginalPosition.y, animationDuration).SetEase(Ease.InOutCirc);
            });
    }

    private bool IsAnimating()
    {
        return _bodyTween != null && _bodyTween.IsActive() && _bodyTween.IsPlaying();
    }
}