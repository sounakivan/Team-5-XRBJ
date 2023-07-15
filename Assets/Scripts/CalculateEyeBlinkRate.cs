using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CalculateEyeBlinkRate : MonoBehaviour
{
    private List<int> LeftBlinksPerSecond = new List<int>();
    private int totalLocalLeftBlinks = 0;
    private int totalBaselineLeftBlinks = 0; 
    public int maxActiveListSize = 10; // Maximum size of the list

    public float Baseline_LeftEyeBlink_Rate;

    public float Active_LeftEyeBlink_Total;
    public float Active_LeftEyeBlink_Rate;
    public float Active_LeftEyeBlink_Ratio;
    public float Active_LeftEyeBlink_PercentIncrease;

    private bool ActiveFlag;  // Flag to indicate if this is during active task, false means baseline.

    void Update()
    {
        if (ActiveFlag == false) //Baseline
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                totalBaselineLeftBlinks++;
            }
        }

        else
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

            // Loop through the list and accumulate the sum
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






//{
//    private float currentTime; // Current time for tracking
//    private float trackingTime = 5f; // The timeframe for tracking the variable
//    private float updateInterval = 0.1f; // The interval between updates
//    private float currentUpdateTime; // Current time for updating

//    private int EyeBlinkCount_Left_GlobalTotal;  // Global Blinks Left Eye
//    private int EyeBlinkCount_Right_GlobalTotal; // Global Blinks Right Eye
//    private int EyeBlinkCount_Both_GlobalTotal;  // Global Blinks Both Eye

//    private int EyeBlinkCount_Left_LocalTotal;  // Local Blinks Left Eye
//    private int EyeBlinkCount_Right_LocalTotal; // Local Blinks Right Eye
//    private int EyeBlinkCount_Both_LocalTotal;  // Local Blinks Both Eyes

//    private int[] EyeBlinkCount_Left_Update;  // Left Eye Blink Update History Array
//    private int[] EyeBlinkCount_Right_Update; // Right Eye Blink Update History Array
//    private int[] EyeBlinkCount_Both_Update;  // Both Eyes Blink Update History Array

//    private int updateIndex;  // Index of the current update value
//    private int BlinkHistory; // How many updates back to use for Local Blink Calculation
//    private bool ActiveFlag;  // Flag to indicate if this is during active task, false means baseline.

//    private float Mean_Baseline_EyeBlink_Total;
//    private float Baseline_EyeBlinkRate;

//    private float Mean_Active_EyeBlink_Total;
//    private float Active_EyeBlinkRate;
//    private float Active_EyeBlinkRatio;
//    private float Active_EyeBlink_Pct;

//    private bool isLeftEyeOpen = false;
//    private bool isRightEyeOpen = false;

//    private void Start()
//    {
//        // Initialize the array to store update values
//        int numberOfUpdates = Mathf.CeilToInt(trackingTime / updateInterval);
//        EyeBlinkCount_Left_Update  = new int[numberOfUpdates];
//        EyeBlinkCount_Right_Update = new int[numberOfUpdates];
//        EyeBlinkCount_Both_Update  = new int[numberOfUpdates];

//        // Start tracking the variable and updating values
//        StartTracking();
//    }

//    private void Update()
//    {
//        //look at eye open data
//        if(isLeftEyeOpen != spacebarpressed && spacebarpressed == closed)
//        {
//            totalLeftBlinks++;
//            //add time to list,
//            isLeftEyeOpen = spaceBarPressed;
//        }



//        // Check if tracking time has elapsed
//        if (currentTime <= 0f)
//        {
//            // Tracking completed, stop updating
//            StopTracking();

//            // Variable tracking finished, use it as desired
//            Debug.Log("Eye Blink Global Total - Left Eye: "  + EyeBlinkCount_Left_GlobalTotal);
//            Debug.Log("Eye Blink Global Total - Right Eye: " + EyeBlinkCount_Right_GlobalTotal);
//            Debug.Log("Eye Blink Global Total - Both Eyes: " + EyeBlinkCount_Both_GlobalTotal);

//            // Output all update values
//            Debug.Log("Update values:");

//            Debug.Log("Eye Blink Update - Left Eye "  + ": " + EyeBlinkCount_Left_Update);
//            Debug.Log("Eye Blink Update - Right Eye " + ": " + EyeBlinkCount_Right_Update);
//            Debug.Log("Eye Blink Update - Both Eyes " + ": " + EyeBlinkCount_Both_Update);


//        }
//        else
//        {

//            // Update the current update value
//            EyeBlinkCount_Left_Update[updateIndex] = // Total number of EyeBlinks Since last update
//            EyeBlinkCount_Right_Update[updateIndex] = // Total number of EyeBlinks Since last update
//            EyeBlinkCount_Both_Update[updateIndex] = EyeBlinkCount_Left_Update[updateIndex] + EyeBlinkCount_Both_Update[updateIndex];// Total number of EyeBlinks Since last update

//            // Update the tracked variable during the tracking time
//            EyeBlinkCount_Left_GlobalTotal  = EyeBlinkCount_Left_GlobalTotal   + EyeBlinkCount_Left_Update[updateIndex]; // Running Tally of Eyeblinks for ALL updates
//            EyeBlinkCount_Right_GlobalTotal = EyeBlinkCount_Right_GlobalTotal  + EyeBlinkCount_Right_Update[updateIndex];// Running Tally of Eyeblinks for ALL updates
//            EyeBlinkCount_Both_GlobalTotal  = EyeBlinkCount_Left_GlobalTotal   + EyeBlinkCount_Right_GlobalTotal;        // Running Tally of Eyeblinks for All updates

//            if (ActiveFlag == true)
//            {
//                // Add the values in the section of the array
//                EyeBlinkCount_Left_LocalTotal = 0;
//                EyeBlinkCount_Right_LocalTotal = 0;

//                for (int i = updateIndex; i <= (updateIndex - BlinkHistory); i++)
//                {

//                    EyeBlinkCount_Left_LocalTotal += EyeBlinkCount_Left_Update[i]; // Tally of EyeBlinks for last X updates
//                    EyeBlinkCount_Right_LocalTotal += EyeBlinkCount_Right_Update[i]; // Tally of EyeBlinks for last X updates
//                }

//                EyeBlinkCount_Both_LocalTotal = EyeBlinkCount_Left_LocalTotal + EyeBlinkCount_Right_LocalTotal;// Running Tally of Eyeblinks for last X updates

//                Debug.Log("Eye Blink Local Total - Left Eye: "  + EyeBlinkCount_Left_LocalTotal);
//                Debug.Log("Eye Blink Local Total - Right Eye: " + EyeBlinkCount_Right_LocalTotal);
//                Debug.Log("Eye Blink Local Total - Both Eyes: " + EyeBlinkCount_Both_LocalTotal);

//                //Calculate Active EyeBlink metrics
//                Mean_Active_EyeBlink_Total = EyeBlinkCount_Both_LocalTotal / 2;
//                Active_EyeBlinkRate = Mean_Active_EyeBlink_Total / trackingTime; // The tracking time needs to be in seconds, I am assuming this is the total time for tracking.
//                Active_EyeBlinkRatio = Active_EyeBlinkRate/ Baseline_EyeBlinkRate;
//                Active_EyeBlink_Pct = Active_EyeBlinkRatio * 100;
//            }

//            // Increment the update index
//            updateIndex++;

//            // Decrement the current time and update time
//            currentTime -= Time.deltaTime;
//            currentUpdateTime -= Time.deltaTime;

//            // Check if it's time for the next update
//            if (currentUpdateTime <= 0f)
//            {
//                // Reset the current update time and index
//                currentUpdateTime = updateInterval;
//                updateIndex++;
//            }
//        }
//    }

//    private void StartTracking()
//    {
//        // Reset the current time, update time, and index
//        currentTime = trackingTime;
//        currentUpdateTime = updateInterval;
//        updateIndex = 0;
//    }

//    private void StopTracking()
//    {
//        //Calculate Active EyeBlink metrics
//        Mean_Baseline_EyeBlink_Total = EyeBlinkCount_Both_GlobalTotal / 2;
//        Baseline_EyeBlinkRate = Mean_Baseline_EyeBlink_Total / trackingTime; // The tracking time needs to be in seconds, I am assuming this is the total time for tracking.
//        // Perform any necessary cleanup or actions after tracking is completed
//        // This function is called when the tracking time has elapsed
//    }
//}