using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum TinyTweenMode
{
    canvas,
    sprite
}

public enum TinyTweenType
{
    scale,
    move,
    rotate,
    canvas_alpha
}

public enum TinyTweenPosition
{
    world,
    local
}

public class TinyTween : MonoBehaviour
{

    [Header("General")]
    [SerializeField] bool active = true;

    [Header("Select Main")]
    [SerializeField] TinyTweenType tweenType = TinyTweenType.move;

    [Header("set -1 to loop infinetly")]
    [SerializeField] int loopValue = 0;

    [Header("Set the Ease type")]
    [SerializeField] Ease easeType = Ease.Linear;
    [SerializeField] LoopType loopType = LoopType.Restart;
    [SerializeField] TinyTweenMode tweenMode = TinyTweenMode.canvas;
    [SerializeField] TinyTweenPosition tweenPosition = TinyTweenPosition.local;

    [Header("Enter Target Values")]
    [SerializeField] Vector3 to = Vector3.zero;
    [SerializeField] float duration = 0f;

    [Header("Move Properties")]
    [SerializeField] bool snapping = false;

    [Header("Rotate Properties")]
    [SerializeField] RotateMode rotateMode;

    [Header("Alpha")]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeTo;
    [SerializeField] float fadeDuration;

    public UnityEvent TweenComplete;

    // Start is called before the first frame update
    void Start()
    {
        if (active)
            StartTween();
    }

    // Update is called once per frame
    void StartTween()
    {
        if (active)
        {
            if (tweenType == TinyTweenType.move)
            {
                if (tweenPosition == TinyTweenPosition.local)
                {
                    transform.DOLocalMove(to, duration, snapping).SetLoops(loopValue, loopType).SetEase(easeType).OnComplete(OnTweenCompleted);
                }
                else
                {
                    transform.DOMove(to, duration, snapping).SetLoops(loopValue, loopType).SetEase(easeType).OnComplete(OnTweenCompleted);
                }
            }
            else if (tweenType == TinyTweenType.scale)
            {
                transform.DOScale(to, duration).SetLoops(loopValue, loopType).SetEase(easeType).OnComplete(OnTweenCompleted);
            }
            else if (tweenType == TinyTweenType.rotate)
            {
                if (tweenPosition == TinyTweenPosition.local)
                {
                    transform.DORotate(to, duration, rotateMode).SetLoops(loopValue, loopType).SetEase(easeType).OnComplete(OnTweenCompleted);
                }
                else
                {
                    transform.DOLocalRotate(to, duration, rotateMode).SetLoops(loopValue, loopType).SetEase(easeType).OnComplete(OnTweenCompleted);
                }
            }
            else if (tweenType == TinyTweenType.canvas_alpha)
            {
                canvasGroup.DOFade(fadeTo, fadeDuration).OnComplete(OnTweenCompleted);
            }
        }
    }

    void OnTweenCompleted()
    {
        if (TweenComplete != null)
            TweenComplete.Invoke();
    }
}