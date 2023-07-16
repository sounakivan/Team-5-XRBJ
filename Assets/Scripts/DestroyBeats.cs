using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBeats : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Beat"))
        {
            Destroy(collision.gameObject);
        }
    }
}
