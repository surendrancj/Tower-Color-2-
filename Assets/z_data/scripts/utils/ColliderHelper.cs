using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderHelper : MonoBehaviour
{
    [SerializeField] string[] filterTags;
    public UnityEvent OnEnter;
    public UnityEvent OnStay;
    public UnityEvent OnExit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        bool isCollided = false;

        if(filterTags.Length<=0)
            isCollided = true;

        foreach(string tag in filterTags){
            if(other.collider.CompareTag(tag))
                isCollided = true;
        }

        if(!isCollided)
            return;

        if(OnEnter != null)
            OnEnter.Invoke();
    }

    void OnCollisionStay(Collision other)
    {
        bool isCollided = false;

        if(filterTags.Length<=0)
            isCollided = true;

        foreach(string tag in filterTags){
            if(other.collider.CompareTag(tag))
                isCollided = true;
        }

        if(!isCollided)
            return;

        if(OnStay != null)
            OnStay.Invoke();
    }

    void OnCollisionExit(Collision other)
    {

        bool isCollided = false;

        if(filterTags.Length<=0)
            isCollided = true;

        foreach(string tag in filterTags){
            if(other.collider.CompareTag(tag))
                isCollided = true;
        }

        if(!isCollided)
            return;
            
        if(OnExit != null)
            OnExit.Invoke();
    }


}
