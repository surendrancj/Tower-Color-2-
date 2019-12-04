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

    [HideInInspector]
    public Color onColor;
    Color onEmissiveColor;
    bool removed = false;
    List<Block> neighbourBlocks;

    public void Setup(Material _mat, Color _color)
    {
        neighbourBlocks = new List<Block>();
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
        if (!removed)
        {
            removed = true;
            GameManager.Instance.blockDestroyDelay += 0.1f;
            Destroy(gameObject, GameManager.Instance.blockDestroyDelay);
            foreach (Block block in neighbourBlocks)
            {
                block.Remove();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("block_enabler") && rigidbody.isKinematic)
        {
            TurnOn();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Block OtherBlock = other.gameObject.GetComponent<Block>();
        if (OtherBlock != null)
        {
            if (!isOn)
                return;

            if (!OtherBlock.onColor.Equals(onColor))
                return;

            if (!neighbourBlocks.Contains(OtherBlock))
            {
                neighbourBlocks.Add(OtherBlock);
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        Block OtherBlock = other.gameObject.GetComponent<Block>();
        if (OtherBlock != null)
        {
            if (!isOn)
                return;

            if (!OtherBlock.onColor.Equals(onColor))
                return;

            if (neighbourBlocks.Contains(OtherBlock))
            {
                neighbourBlocks.Remove(OtherBlock);
            }
        }
    }
}
