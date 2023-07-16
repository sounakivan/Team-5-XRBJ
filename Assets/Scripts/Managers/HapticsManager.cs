using UnityEngine;
using UnityEngine.XR.MagicLeap;
using static UnityEngine.XR.MagicLeap.InputSubsystem.Extensions.Haptics;

public class HapticFeedbackManager : MonoBehaviour
{
    [SerializeField] HapticData hoverFeedback;
    [SerializeField] HapticData selectFeedback;

    PreDefined predefinedHoverBuzz;
    PreDefined predefinedSelectionBuzz;
    Buzz hoverBuzz;
    Buzz selectionBuzz;

    static HapticFeedbackManager singletonInstance;

    [System.Serializable]
    public class HapticData
    {
        public ushort initHz;
        public ushort finalHz;
        public ushort durationInMs;
        public byte intensity;

        public HapticData(ushort initHz, ushort finalHz, ushort durationInMs, byte intensity)
        {
            this.initHz = initHz;
            this.finalHz = finalHz;
            this.durationInMs = durationInMs;
            this.intensity = intensity;
        }
    }

    public static HapticFeedbackManager Singleton
    {
        get
        {
            if (singletonInstance == null)
            {
                singletonInstance = FindObjectOfType<HapticFeedbackManager>();

                if (singletonInstance == null)
                {
                    GameObject newSingletonObject = new GameObject("HapticFeedbackManager");
                    singletonInstance = newSingletonObject.AddComponent<HapticFeedbackManager>();
                }
            }

            return singletonInstance;
        }
    }

    void Awake()
    {
        if (singletonInstance != null && singletonInstance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        selectionBuzz = Buzz.Create(selectFeedback.initHz, selectFeedback.finalHz, selectFeedback.durationInMs, selectFeedback.intensity);
        hoverBuzz = Buzz.Create(hoverFeedback.initHz, hoverFeedback.finalHz, hoverFeedback.durationInMs, hoverFeedback.intensity);
        predefinedSelectionBuzz = PreDefined.Create(PreDefined.Type.B);
        predefinedHoverBuzz = PreDefined.Create(PreDefined.Type.A);
    }

    public void EndFeedback()
    {
        Stop();
    }

    public void InitCustomBuzz(HapticData customFeedback)
    {
        if (customFeedback.initHz == 0 && customFeedback.finalHz == 0 && customFeedback.durationInMs == 0 && customFeedback.intensity == 0)
        {
            Debug.LogWarning("Please provide valid values for the custom feedback.");
            return;
        }

        Buzz customBuzz = Buzz.Create(customFeedback.initHz, customFeedback.finalHz, customFeedback.durationInMs, customFeedback.intensity);
        customBuzz.StartHaptics();
    }

    public void InitHoverBuzz()
    {
        hoverBuzz.StartHaptics();
    }

    public void InitSelectBuzz()
    {
        selectionBuzz.StartHaptics();
    }

    public void InitPredefinedSelectBuzz()
    {
        predefinedSelectionBuzz.StartHaptics();
    }

    public void InitPredefinedHoverBuzz()
    {
        predefinedHoverBuzz.StartHaptics();
    }
}
