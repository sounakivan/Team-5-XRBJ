using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.XR;
using TMPro;
using static MagicLeapInputs;

public class MagicLeapInputManager : MonoBehaviour
{
    [SerializeField, Tooltip("Left Eye Statistic Panel")]
    TextMeshProUGUI _leftEyeTextStatic;
    [SerializeField, Tooltip("Right Eye Statistic Panel")]
    TextMeshProUGUI _rightEyeTextStatic;
    [SerializeField, Tooltip("Both Eyes Statistic Panel")]
    TextMeshProUGUI _bothEyesTextStatic;
    [SerializeField, Tooltip("Fixation Point marker")]
    Transform _eyesFixationPointMarker;
    [SerializeField]
    LineRenderer _eyeTrackingDebugLine;

    UnityEngine.InputSystem.XR.Eyes _eyeData;
    Vector3 _leftEyeForwardGaze;

    MagicLeapInputs mlInputs;
    MagicLeapInputs.EyesActions eyesActions;

    // Used to get other eye data
    InputDevice eyesDevice;

    // Was EyeTracking permission granted by user
    bool permissionGranted = false;
    readonly MLPermissions.Callbacks permissionCallbacks = new MLPermissions.Callbacks();

    //getters/setters
    public UnityEngine.InputSystem.XR.Eyes EyeData => _eyeData;
    public Vector3 LeftEyeForwardGaze => _leftEyeForwardGaze;

    static MagicLeapInputManager _instance;

    public static MagicLeapInputManager Instance
    {
        get
        {
            // If the instance is null or not destroyed, find or create a new instance
            if (_instance == null || ReferenceEquals(_instance, null))
            {
                _instance = FindObjectOfType<MagicLeapInputManager>();

                // If no instance exists in the scene, create a new GameObject and attach the script
                if (_instance == null || ReferenceEquals(_instance, null))
                {
                    GameObject singletonObject = new GameObject("MagicLeapInputManager");
                    _instance = singletonObject.AddComponent<MagicLeapInputManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        // Ensure only one instance of the HapticsManager exists in the scene
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        permissionCallbacks.OnPermissionGranted += OnPermissionGranted;
        permissionCallbacks.OnPermissionDenied += OnPermissionDenied;
        permissionCallbacks.OnPermissionDeniedAndDontAskAgain += OnPermissionDenied;
    }

    private void Start()
    {
        mlInputs = new MagicLeapInputs();
        mlInputs.Enable();

        _eyeTrackingDebugLine.positionCount = 2;

#if !UNITY_EDITOR
            MLPermissions.RequestPermission(MLPermission.EyeTracking, permissionCallbacks);
#endif
    }

    private void OnDestroy()
    {
        permissionCallbacks.OnPermissionGranted -= OnPermissionGranted;
        permissionCallbacks.OnPermissionDenied -= OnPermissionDenied;
        permissionCallbacks.OnPermissionDeniedAndDontAskAgain -= OnPermissionDenied;

        mlInputs.Disable();
        mlInputs.Dispose();

        InputSubsystem.Extensions.MLEyes.StopTracking();
    }

    private void Update()
    {
        if (!permissionGranted)
        {
            return;
        }

        if (!eyesDevice.isValid)
        {
            this.eyesDevice = InputSubsystem.Utils.FindMagicLeapDevice(InputDeviceCharacteristics.EyeTracking | InputDeviceCharacteristics.TrackedDevice);
            return;
        }

        MLResult gazeStateResult = MLGazeRecognition.GetState(out MLGazeRecognition.State state);
        MLResult gazeStaticDataResult = MLGazeRecognition.GetStaticData(out MLGazeRecognition.StaticData data);

        Debug.Log($"MLGazeRecognitionStaticData {gazeStaticDataResult.Result}\n" +
            $"Vergence {data.Vergence}\n" +
            $"EyeHeightMax {data.EyeHeightMax}\n" +
            $"EyeWidthMax {data.EyeWidthMax}\n" +
            $"MLGazeRecognitionState: {gazeStateResult.Result}\n" +
            state.ToString());

        // Eye data provided by the engine for all XR devices.
        // Used here only to update the status text. The 
        // left/right eye centers are moved to their respective positions &
        // orientations using InputSystem's TrackedPoseDriver component.
        _eyeData = eyesActions.Data.ReadValue<UnityEngine.InputSystem.XR.Eyes>();
        

        // Manually set fixation point marker so we can apply rotation, since UnityXREyes
        // does not provide it
        _eyesFixationPointMarker.position = _eyeData.fixationPoint;
        _eyesFixationPointMarker.rotation = Quaternion.LookRotation(_eyeData.fixationPoint - Camera.main.transform.position);

        // Eye data specific to Magic Leap
        InputSubsystem.Extensions.TryGetEyeTrackingState(eyesDevice, out var trackingState);

        _leftEyeForwardGaze = _eyeData.leftEyeRotation * Vector3.forward;

        string leftEyeText =
            $"Center:\n({_eyeData.leftEyePosition.x:F2}, {_eyeData.leftEyePosition.y:F2}, {_eyeData.leftEyePosition.z:F2})\n" +
            $"Gaze:\n({_leftEyeForwardGaze.x:F2}, {_leftEyeForwardGaze.y:F2}, {_leftEyeForwardGaze.z:F2})\n" +
            $"Confidence:\n{trackingState.LeftCenterConfidence:F2}\n" +
            $"Openness:\n{_eyeData.leftEyeOpenAmount:F2}";

        _leftEyeTextStatic.text = leftEyeText;

        var rightEyeForwardGaze = _eyeData.rightEyeRotation * Vector3.forward;

        string rightEyeText =
            $"Center:\n({_eyeData.rightEyePosition.x:F2}, {_eyeData.rightEyePosition.y:F2}, {_eyeData.rightEyePosition.z:F2})\n" +
            $"Gaze:\n({rightEyeForwardGaze.x:F2}, {rightEyeForwardGaze.y:F2}, {rightEyeForwardGaze.z:F2})\n" +
            $"Confidence:\n{trackingState.RightCenterConfidence:F2}\n" +
            $"Openness:\n{_eyeData.rightEyeOpenAmount:F2}\n" +
            $"MLGazeRecognitionStaticData {gazeStaticDataResult.Result}\n" +
            $"Vergence {data.Vergence}\n" +
            $"EyeHeightMax {data.EyeHeightMax}\n" +
            $"EyeWidthMax {data.EyeWidthMax}\n" +
            $"MLGazeRecognitionState: {gazeStateResult.Result}\n" +
            state.ToString();

        _rightEyeTextStatic.text = rightEyeText;

        string bothEyesText =
            $"Fixation Point:\n({_eyeData.fixationPoint.x:F2}, {_eyeData.fixationPoint.y:F2}, {_eyeData.fixationPoint.z:F2})\n" +
            $"Confidence:\n{trackingState.FixationConfidence:F2}";

        _bothEyesTextStatic.text = $"{bothEyesText}";

        if (trackingState.RightBlink || trackingState.LeftBlink)
        {
            Debug.Log($"Eye Tracking Blink Registered Right Eye Blink: {trackingState.RightBlink} Left Eye Blink: {trackingState.LeftBlink}");
        }

        //debug line renderer
        _eyeTrackingDebugLine.SetPosition(0, Camera.main.transform.position); // Set the start position of the line to be the camera's position
        _eyeTrackingDebugLine.SetPosition(1, _eyeData.fixationPoint); // Set the end position of the line to be the fixation point
    }

    private void OnPermissionDenied(string permission)
    {
        MLPluginLog.Error($"{permission} denied, example won't function.");
    }

    private void OnPermissionGranted(string permission)
    {
        InputSubsystem.Extensions.MLEyes.StartTracking();
        eyesActions = new MagicLeapInputs.EyesActions(mlInputs);
        permissionGranted = true;
    }
}
