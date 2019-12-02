using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int id;
    [SerializeField] Rigidbody rigidbody;

    public void Setup(Material _mat, Color _color)
    {
        // set the color and other material poperties
        Renderer rr = gameObject.GetComponent<Renderer>();
        rr.material.CopyPropertiesFromMaterial(_mat);
        rr.material.color = _color;
        rr.material.SetColor("_EmissionColor", _color * 0.4f);
    }
}
