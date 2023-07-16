using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBeats : MonoBehaviour
{
    public AudioClip destroySound; // Assign the sound effect in the Inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Beat"))
        {
            Destroy(collision.gameObject);
        }
    }
}
