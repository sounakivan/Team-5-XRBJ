using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CalculateActiveEyeBlinkRate : MonoBehaviour
{
    private List<int> LeftBlinksPerSecond = new List<int>();
    private int totalLocalLeftBlinks = 0;
    public int maxActiveListSize = 10; // Maximum size of the list

    private float Active_LeftEyeBlink_Total;
    private float Active_LeftEyeBlink_Rate;
    private float Active_LeftEyeBlink_Ratio;
    private float Active_LeftEyeBlink_PercentIncrease;
    private float Baseline_LeftEyeBlink_Rate = CalculateBaselineEyeblinkRate.Baseline_LeftEyeBlink_Rate;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            totalLocalLeftBlinks++;
        }

        if (Time.frameCount % 60 == 0) // Calculate presses per second every 60 frames (1 second)
        {
            LeftBlinksPerSecond.Add(totalLocalLeftBlinks);
            totalLocalLeftBlinks = 0;

            // If the list size exceeds the maximum, remove the oldest value
            if (LeftBlinksPerSecond.Count > maxActiveListSize)
            {
                LeftBlinksPerSecond.RemoveAt(0);
            }

            Debug.Log("Active Task, Local Left Blinks per Second: " + string.Join(", ", LeftBlinksPerSecond));
        }

        // Calculate Active, Local Blink Metrics
        if (LeftBlinksPerSecond.Count == maxActiveListSize)
        {

        // Loop through the list and accumulate the sum
        Active_LeftEyeBlink_Total = 0;
        for (int i = 0; i < LeftBlinksPerSecond.Count; i++)
        {
            Active_LeftEyeBlink_Total += LeftBlinksPerSecond[i];
        }

        Active_LeftEyeBlink_Rate = Active_LeftEyeBlink_Total / LeftBlinksPerSecond.Count; // The tracking time needs to be in seconds, I am assuming this is the total time for tracking.
        Active_LeftEyeBlink_Ratio = Active_LeftEyeBlink_Rate / Baseline_LeftEyeBlink_Rate;
        Active_LeftEyeBlink_PercentIncrease = Active_LeftEyeBlink_Ratio * 100;

        Debug.Log("Active Left Eye Blink Total: " + Active_LeftEyeBlink_Total);
        Debug.Log("Active Left Eye Blink Rate: " + Active_LeftEyeBlink_Rate);
        Debug.Log("Active Left Eye Blink Ratio: " + Active_LeftEyeBlink_Ratio);
        Debug.Log("Active Left Eye Blink Percent Increase: " + Active_LeftEyeBlink_PercentIncrease);

        }
    }
}