using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraController : MonoBehaviour
{
    public event Action OnMoveToStartCompleted;
    [HideInInspector] public bool canSwipe = false;

    [SerializeField] Camera mainCam;
    [SerializeField] float topPos = 0f;
    [SerializeField] float moveToTopDuration = 1f;
    [SerializeField] Transform followTr;
    [SerializeField] float followSpeed = 2f;
    [SerializeField] float folloOffset = 0f;
    [SerializeField] float camMinY = 0f;

    float camMaxY = 0f;
    float xAngle = 0f;
    float yAngle = 0f;
    float xAngTemp = 0f;
    float yAngTemp = 0f;
    Vector3 topRotation;
    Vector3 firstpoint;
    Vector3 secondpoint;
    float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        camMaxY = topPos;
        canSwipe = false;
        transform.position = Vector3.zero;
        MoveToTop();

        xAngle = 0f;
        yAngle = 0f;
    }

    void Update()
    {
        // rotate camra horizonatly 
        if (canSwipe)
        {
            SwipeRotateCamera();

            float interpolation = followSpeed * Time.deltaTime;
            Vector3 position = transform.position;
            position.y = Mathf.Lerp(this.transform.position.y, followTr.transform.position.y + folloOffset, interpolation);
            if (position.y > camMinY && position.y < camMaxY)
                transform.position = position;
        }
    }

    void SwipeRotateCamera()
    {
        //Check count touches
        if (Input.touchCount > 0)
        {
            //Touch began, save position
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firstpoint = Input.GetTouch(0).position;
                xAngTemp = xAngle;
                yAngTemp = yAngle;
            }
            //Move finger by screen
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                secondpoint = Input.GetTouch(0).position;
                //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
                xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 180f / Screen.width;
                yAngle = yAngTemp - (secondpoint.y - firstpoint.y) * 90f / Screen.height;
                //Rotate camera
                transform.rotation = Quaternion.Euler(0f, xAngle, 0f);
            }
        }
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
