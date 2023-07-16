using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBeat : MonoBehaviour
{
    public AudioClip destroySound; // Assign the sound effect in the Inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Traveller"))
        {
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(destroySound);
        }
    }
}
