using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveMovement : MonoBehaviour
{
    [SerializeField] CalculateActiveEyeBlinkRate _calculateActiveEyeBlinkRate;

    float Active_LeftEyeBlink_Ratio; //Gets smaller as active blink rate increases
    public float HeartRateVariable;  

    public float xFrequency = 10f;     // Adjust the frequency of the sine wave for X-axis
    public float xAmplitude = 5f;    // Adjust the amplitude of the sine wave for X-axis

     float _yFrequency = 2f;   // Adjust the frequency of the sine wave for Y-axis
     float _yAmplitude = .03f;     // Adjust the amplitude of the sine wave for Y-axis

    float time = 0f;
    Vector3 originalPosition;

    public float moveSpeed = 5f; // Adjust this value to control the movement speed //og 5
    Rigidbody beat;

    void Start()
    {
        _calculateActiveEyeBlinkRate = FindAnyObjectByType<CalculateActiveEyeBlinkRate>(); //TODO: gross fix this

        HeartRateVariable = 1f; //DEBUG, no heartbeat data

        beat = GetComponent<Rigidbody>();
        beat.position = new Vector3(0, 0, -(_yAmplitude * Active_LeftEyeBlink_Ratio)/2);
        beat.velocity = new Vector3(moveSpeed, 0f, 0f);

        originalPosition = transform.position;
        
        Active_LeftEyeBlink_Ratio = _calculateActiveEyeBlinkRate.Active_LeftEyeBlink_Ratio;
        //DEBUG
        /*if (Active_LeftEyeBlink_Ratio > 1 || Active_LeftEyeBlink_Ratio < 0)
        {
            Active_LeftEyeBlink_Ratio = .5f;
        }*/
    }

    void Update()
    {
        Debug.Log("Active Left Eye Blink Ratio: " + Active_LeftEyeBlink_Ratio);

        // Adjust the X-axis and Y-axis values based on independent sine waves
        if(Active_LeftEyeBlink_Ratio > 1)
        {
            Active_LeftEyeBlink_Ratio = 1;
        }

        // AMPLITUDE IS ADJUSTED BY BLINK RATE -- A FASTER ACTIVE BLINK RATE DECREASES THE AMPLITUDE
        // FREQUENCY IS ADJUSTED BY HEART RATE -- SLOWER HEART RATE DECREASES FREQUENCY
        float x = originalPosition.x + (xAmplitude* Active_LeftEyeBlink_Ratio) * Mathf.Sin(2f * Mathf.PI * HeartRateVariable * time);
        float newYAmplitude = _yAmplitude * Active_LeftEyeBlink_Ratio;
        float y = /*originalPosition.y +*/ (_yAmplitude* Active_LeftEyeBlink_Ratio) * Mathf.Cos(2f * Mathf.PI * HeartRateVariable * time);

        // Update the object's position with the adjusted X-axis and Y-axis values
        //transform.position = new Vector3(x, y, originalPosition.z);d
        transform.position += new Vector3(0, 0, y); //-yAmplitude + 

        // Increment time
        time += Time.deltaTime;
    }
}
