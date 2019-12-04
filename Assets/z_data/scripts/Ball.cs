using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void OnCollisionEnter(Collision _other)
    {
        if (_other.collider.CompareTag("block"))
        {
            Color blockColor = _other.gameObject.GetComponent<Renderer>().material.color;
            Color ballColor = gameObject.GetComponent<Renderer>().material.color;
            if (ballColor.Equals(blockColor))
            {
                RemoveBlocks(_other);
            }
        }
    }

    void RemoveBlocks(Collision _other)
    {
        _other.gameObject.GetComponent<Block>().Remove();
        Destroy(gameObject, 1f);
    }

    // void Explode(Collision _other)
    // {
    //     Vector3 explosionPos = transform.position;
    //     float radius = 0.8f;
    //     float explosionForce = 200f;
    //     Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
    //     foreach (Collider hit in colliders)
    //     {
    //         Rigidbody rb = hit.GetComponent<Rigidbody>();

    //         if (rb != null)
    //             rb.AddExplosionForce(explosionForce, explosionPos, radius, 3.0F);
    //     }
    //     gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //     Destroy(_other.gameObject);
    //     Destroy(gameObject);
    // }
}
