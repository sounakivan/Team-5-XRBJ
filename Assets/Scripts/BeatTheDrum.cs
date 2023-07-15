using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BeatTheDrum : MonoBehaviour
{
    public GameObject beatPrefab;
    public GameObject beatEffectPrefab;
    public Transform drum;
    public Transform beatEffect;
    private Vector3 drumPos;
    private Vector3 beatEffectPos;
    private bool hasSpawnedBeat = false;

    public void Start()
    {
        drumPos = drum.position;
        beatEffectPos = beatEffect.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision is happening with the desired object
        if (other.gameObject.CompareTag("Guide"))
        {
            // Instantiate the beatPrefab at the collision point
            sendBeat();
        }
    }

    private void sendBeat()
    {
        Instantiate(beatPrefab, drumPos, Quaternion.identity);
        Instantiate(beatEffectPrefab, beatEffectPos, Quaternion.Euler(-90,0,0));
        hasSpawnedBeat = true;
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset the flag when exiting the collision
        if (other.gameObject.CompareTag("Guide"))
        {
            hasSpawnedBeat = false;
        }
    }
}

