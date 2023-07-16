using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveMovement : MonoBehaviour
{

    internal float Active_LeftEyeBlink_Rate             = CalculateActiveEyeBlinkRate.Active_LeftEyeBlink_Rate;
    internal float Active_LeftEyeBlink_Ratio            = CalculateActiveEyeBlinkRate.Active_LeftEyeBlink_Ratio; //Gets smaller as active blink rate increases
    internal float Active_LeftEyeBlink_PercentIncrease  = CalculateActiveEyeBlinkRate.Active_LeftEyeBlink_PercentIncrease;
    internal float Baseline_LeftEyeBlink_Rate           = CalculateBaselineEyeBlinkRate.Baseline_LeftEyeBlink_Rate;
    public float HeartRateVariable;  

    public float xFrequency = 10f;     // Adjust the frequency of the sine wave for X-axis
    public float xAmplitude = 5f;    // Adjust the amplitude of the sine wave for X-axis

    public float yFrequency = 10f;   // Adjust the frequency of the sine wave for Y-axis
    public float yAmplitude = 5f;     // Adjust the amplitude of the sine wave for Y-axis

    private float time = 0f;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {

        Debug.Log("Active Left Eye Blink Ratio: " + Active_LeftEyeBlink_Ratio);

        // Adjust the X-axis and Y-axis values based on independent sine waves

        // AMPLITUDE IS ADJUSTED BY BLINK RATE -- A FASTER ACTIVE BLINK RATE DECREASES THE AMPLITUDE
        // FREQUENCY IS ADJUSTED BY HEART RATE -- SLOWER HEART RATE DECREASES FREQUENCY
        float x = originalPosition.x + (xAmplitude* Active_LeftEyeBlink_Ratio) * Mathf.Sin(2f * Mathf.PI * HeartRateVariable * time);
        float y = originalPosition.y + (yAmplitude* Active_LeftEyeBlink_Ratio) * Mathf.Sin(2f * Mathf.PI * HeartRateVariable * time);

        // Update the object's position with the adjusted X-axis and Y-axis values
        transform.position = new Vector3(x, y, originalPosition.z);

        // Increment time
        time += Time.deltaTime;
    }
}
