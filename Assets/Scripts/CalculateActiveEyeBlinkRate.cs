using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

public class CalculateActiveEyeBlinkRate : MonoBehaviour
{
    private List<int> LeftBlinksPerSecond = new List<int>();
    private int totalLocalLeftBlinks = 0;
    public int maxActiveListSize = 10; // Maximum size of the list

    float Active_LeftEyeBlink_Total;
    float Active_LeftEyeBlink_Rate;
    float _active_LeftEyeBlink_Ratio;
    float Active_LeftEyeBlink_PercentIncrease;
    float Baseline_LeftEyeBlink_Rate;

    [SerializeField] CalculateBaselineEyeBlinkRate calculateBaselineEyeBlinkRate;

    bool _isEyeOpen = false;

    public float Active_LeftEyeBlink_Ratio => _active_LeftEyeBlink_Ratio;

    void Start()
    {
        Baseline_LeftEyeBlink_Rate = calculateBaselineEyeBlinkRate.Baseline_LeftEyeBlink_Rate;

        if (Baseline_LeftEyeBlink_Rate == 0)
        {
            Baseline_LeftEyeBlink_Rate = .2f;
        }
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("blink");
            totalLocalLeftBlinks++;
        }*/

        bool currentIsEyeOpen = MagicLeapInputManager.Instance.TrackingState.LeftBlink;
        Debug.Log("leftcenterconfidence: " + MagicLeapInputManager.Instance.TrackingState.LeftCenterConfidence);
        Debug.Log("leftopen: " + MagicLeapInputManager.Instance.TrackingState.LeftBlink);

        if (_isEyeOpen && !currentIsEyeOpen)
        {
            totalLocalLeftBlinks++;
        }

        _isEyeOpen = currentIsEyeOpen;

        //TODO: change that 60 fps
        if (Time.frameCount % 60 == 0) // Calculate presses per second every 60 frames (1 second)
        {
            LeftBlinksPerSecond.Add(totalLocalLeftBlinks);
            totalLocalLeftBlinks = 0;

            // If the list size exceeds the maximum, remove the oldest value
            if (LeftBlinksPerSecond.Count > maxActiveListSize)
            {
                LeftBlinksPerSecond.RemoveAt(0);
            }

            //Debug.Log("Active Task, Local Left Blinks per Second: " + string.Join(", ", LeftBlinksPerSecond));
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
            _active_LeftEyeBlink_Ratio = Baseline_LeftEyeBlink_Rate / Active_LeftEyeBlink_Rate; // Gets smaller as Active BlinkRate increases
            Active_LeftEyeBlink_PercentIncrease = _active_LeftEyeBlink_Ratio * 100;

            Debug.Log("Active Left Eye Blink Total: " + Active_LeftEyeBlink_Total);
            Debug.Log("Active Left Eye Blink Rate: " + Active_LeftEyeBlink_Rate);
            Debug.Log("Active Left Eye Blink Ratio: " + _active_LeftEyeBlink_Ratio);
            Debug.Log("Active Left Eye Blink Percent Increase: " + Active_LeftEyeBlink_PercentIncrease);

        }
    }
}