using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class TriggerEvent : UnityEvent<Collider>
{
}

public class TriggerHelper : MonoBehaviour
{
    [SerializeField] string[] filterTags;
    public TriggerEvent OnEnter;
    public TriggerEvent OnStay;
    public TriggerEvent OnExit;

    void OnTriggerEnter(Collider _other)
    {
        bool isCollided = false;

        if (filterTags.Length <= 0)
            isCollided = true;

        foreach (string tag in filterTags)
        {
            if (_other.CompareTag(tag))
                isCollided = true;
        }

        if (!isCollided)
            return;

        if (OnEnter != null)
            OnEnter.Invoke(_other);
    }

    void OnTriggerStay(Collider _other)
    {
        bool isCollided = false;

        if (filterTags.Length <= 0)
            isCollided = true;

        foreach (string tag in filterTags)
        {
            if (_other.CompareTag(tag))
                isCollided = true;
        }

        if (!isCollided)
            return;

        if (OnStay != null)
            OnStay.Invoke(_other);
    }

    void OnTriggerExit(Collider _other)
    {
        bool isCollided = false;

        if (filterTags.Length <= 0)
            isCollided = true;

        foreach (string tag in filterTags)
        {
            if (_other.CompareTag(tag))
                isCollided = true;
        }

        if (!isCollided)
            return;

        if (OnExit != null)
            OnExit.Invoke(_other);
    }
}
