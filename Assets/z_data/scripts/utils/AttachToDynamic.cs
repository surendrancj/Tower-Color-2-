using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToDynamic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start ()
    {
        gameObject.transform.parent = GameObject.Find ("dynamic_objects").transform;
    }
}