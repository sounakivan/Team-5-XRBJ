using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateBaselineEyeBlinkRate : MonoBehaviour
{

    private int totalBaselineLeftBlinks = 0;
    internal static float Baseline_LeftEyeBlink_Rate;
    private float StartTime;
    private float ElapsedTime;
    public float BaselineTime;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime = Time.time - StartTime; //I "Think" this is in seconds. Double check

        if (ElapsedTime >= BaselineTime)
        {
            StopCount();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                totalBaselineLeftBlinks++;
                Debug.Log("Baseline Left Eye Blink Total: " + totalBaselineLeftBlinks);

            }

        }

         
    }
    

    private void StopCount()
    {

        Baseline_LeftEyeBlink_Rate = totalBaselineLeftBlinks / ElapsedTime; // The tracking time needs to be in seconds, I am assuming this is the total time for tracking.
        
        Debug.Log("Baseline Left Eye Blink Rate: " + Baseline_LeftEyeBlink_Rate);

        enabled = false;

    }
}
