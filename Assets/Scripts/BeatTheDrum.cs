using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTheDrum : MonoBehaviour
{
    public GameObject beatPrefab;
    public Transform drum;
    private Vector3 drumPos;

    public void Start()
    {
        drumPos = drum.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is happening with the desired object
        if (collision.gameObject.CompareTag("Guide"))
        {
            // Instantiate the beatPrefab at the collision point
            Instantiate(beatPrefab, drumPos, Quaternion.identity);
        }
    }
}

