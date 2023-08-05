using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeagullBehaviour : MonoBehaviour
{
    [Header("Settings")] public KeyCode keyCode;
    public float animationDuration = 0.5f;
    float animationHeight = 1.53f;
    [Header("Sprite")] public Sprite idleSprite;
    public Sprite pressedSprite;
    [Header("Information")] public State currentState = State.Idle;
    private Vector3 _bodyOriginalPosition;
    private Tween _bodyTween;
    private bool _isAnimating;
    private Rigidbody2D _rb;
    private TextMeshProUGUI _keyCode;
    private SpriteRenderer _spriteRenderer;
    private TextMeshPro _text;

    public enum State
    {
        Idle,
        Pressed
    }

    private void Awake()
    {
        _text = GameObject.Find("KeyCode").GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        _bodyOriginalPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _text.text = keyCode.ToString();
        _spriteRenderer = transform.Find("Body").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && !IsAnimating())
        {
            _bodyTween.Kill();
            StartRigidAnimation();
        }

        _spriteRenderer.sprite = !IsAnimating() ? idleSprite : pressedSprite;
    }

    private void StartRigidAnimation()
    {
        currentState = State.Pressed;
        _bodyTween = _rb.DOMoveY(_bodyOriginalPosition.y + animationHeight, animationDuration)
            .SetEase(Ease.InOutCirc)
            .OnComplete(() =>
            {
                _rb.DOMoveY(_bodyOriginalPosition.y, animationDuration).SetEase(Ease.InOutCirc);
            });
    }

    private bool IsAnimating()
    {
        return _bodyTween != null && _bodyTween.IsActive() && _bodyTween.IsPlaying();
    }
}