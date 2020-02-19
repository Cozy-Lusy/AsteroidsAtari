using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void FixedUpdate()
    {
        Destroy(gameObject, 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Asteroid>() != null)
        {
            Destroy(gameObject);
        }
    }
}
