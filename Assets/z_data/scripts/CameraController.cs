using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraController : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] float topPos = 0f;
    [SerializeField] float moveToTopDuration = 1f;
    [SerializeField] Vector3 topRotation;

    public event Action OnMoveToStartCompleted;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        MoveToTop();
    }

    void MoveToTopCompleted()
    {
        if (OnMoveToStartCompleted != null)
            OnMoveToStartCompleted();
    }

    public void MoveToTop()
    {
        transform.DOMoveY(topPos, moveToTopDuration).OnComplete(MoveToTopCompleted);
        transform.DORotate(topRotation, moveToTopDuration);
    }
}
