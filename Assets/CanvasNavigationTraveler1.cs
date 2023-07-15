using UnityEngine;
using UnityEngine.UI;

public class CanvasNavigationTraveler1 : MonoBehaviour
{
    public Canvas[] canvases;
    public Button[] switchButtons;

    private int activeCanvasIndex;

    private void Start()
    {
        // Set initial active canvas
        activeCanvasIndex = 0;

        // Attach button click events
        for (int i = 0; i < switchButtons.Length; i++)
        {
            int currentIndex = i;
            switchButtons[i].onClick.AddListener(() => SwitchCanvas(currentIndex));
        }

        // Initialize canvases
        SetCanvasVisibility();
    }

    private void SwitchCanvas(int newIndex)
    {
        // Deactivate current active canvas
        canvases[activeCanvasIndex].gameObject.SetActive(false);

        // Set new active canvas index
        activeCanvasIndex = newIndex;

        // Activate new active canvas
        canvases[activeCanvasIndex].gameObject.SetActive(true);
    }

    private void SetCanvasVisibility()
    {
        // Disable all canvases except the active one
        for (int i = 0; i < canvases.Length; i++)
        {
            if (i != activeCanvasIndex)
            {
                canvases[i].gameObject.SetActive(false);
            }
            else
            {
                canvases[i].gameObject.SetActive(true);
            }
        }
    }
}
