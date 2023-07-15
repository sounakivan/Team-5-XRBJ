using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellingBeat : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to control the movement speed
    private Rigidbody beat;

    private void Start()
    {
        beat = GetComponent<Rigidbody>();
        beat.velocity = new Vector3(moveSpeed, 0f, 0f);
    }
}
