using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateBaselineEyeBlinkRate : MonoBehaviour
{
    float BaselineTime; //how many secs eye blinks are recorded for

    int _totalBaselineLeftBlinks = 0;
    float _baseline_LeftEyeBlink_Rate;
    float StartTime;
    float ElapsedTime;

    bool _isEyeOpen = false;

    public float TotalBaselineLeftBlinks => _totalBaselineLeftBlinks;
    public float Baseline_LeftEyeBlink_Rate => _baseline_LeftEyeBlink_Rate;

    private void OnEnable()
    {
        //TODO: make sure headset is on player's head
        StartTime = Time.time;
    }

    void OnDisable()
    {
        ElapsedTime = Time.time - StartTime;
        StopCount();
    }

    void Update()
    {
        ElapsedTime = Time.time - StartTime; //I "Think" this is in seconds. Double check
        /*
        if (Input.GetKeyDown(KeyCode.B))
        {
            _totalBaselineLeftBlinks++;
            Debug.Log("Baseline Left Eye Blink Total: " + _totalBaselineLeftBlinks);
        }*/
        
        bool currentIsEyeOpen = MagicLeapInputManager.Instance.TrackingState.LeftBlink;
        if (_isEyeOpen && !currentIsEyeOpen)
        {
            _totalBaselineLeftBlinks++;
            Debug.Log("Baseline Left Eye Blink Total: " + _totalBaselineLeftBlinks);
        }

        _isEyeOpen = currentIsEyeOpen;
    }

    private void StopCount()
    {
        _baseline_LeftEyeBlink_Rate = _totalBaselineLeftBlinks / ElapsedTime; // The tracking time needs to be in seconds, I am assuming this is the total time for tracking.

        Debug.Log("Baseline Left Eye Blink Rate: " + _baseline_LeftEyeBlink_Rate);

        enabled = false;
    }
}
