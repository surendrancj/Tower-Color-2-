using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int id;

    public bool isOn
    {
        get => !rigidbody.isKinematic;
    }

    public bool isRemoved
    {
        get => removed;
    }

    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Color offEmisssiveColor;
    [SerializeField] Color offColor = Color.black;

    Color onColor;
    Color onEmissiveColor;
    bool removed = false;

    public void Setup(Material _mat, Color _color)
    {
        removed = false;
        // set the color and other material poperties
        Renderer rr = gameObject.GetComponent<Renderer>();
        rr.material.CopyPropertiesFromMaterial(_mat);
        rr.material.color = _color;
        onEmissiveColor = _color * 0.4f;
        rr.material.SetColor("_EmissionColor", onEmissiveColor);
        onColor = _color;
    }

    public void TurnOff()
    {
        rigidbody.isKinematic = true;
        Renderer rr = gameObject.GetComponent<Renderer>();
        rr.material.SetColor("_EmissionColor", offEmisssiveColor);
        rr.material.color = offColor;
    }

    public void TurnOn()
    {
        rigidbody.isKinematic = false;
        Renderer rr = gameObject.GetComponent<Renderer>();
        rr.material.color = onColor;
        rr.material.SetColor("_EmissionColor", onEmissiveColor);
    }

    public void Remove()
    {
        removed = true;
        TurnOff();
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("block_enabler") && rigidbody.isKinematic)
        {
            TurnOn();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("block_enabler") && rigidbody.isKinematic)
        {
            TurnOn();
        }
    }
}
