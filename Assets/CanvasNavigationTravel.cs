using UnityEngine;
using UnityEngine.UI;

public class CanvasNavigationTravel : MonoBehaviour
{
    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Button switchButton;

    private Canvas activeCanvas;

    private void Start()
    {
        // Set initial active canvas
        activeCanvas = canvas1;

        // Attach button click event
        switchButton.onClick.AddListener(SwitchCanvas);

        // Initialize canvases
        canvas1.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);
        canvas3.gameObject.SetActive(false);
    }

    private void SwitchCanvas()
    {
        // Deactivate current active canvas
        activeCanvas.gameObject.SetActive(false);

        // Switch active canvas
        if (activeCanvas == canvas1)
        {
            activeCanvas = canvas2;
        }
        else if (activeCanvas == canvas2)
        {
            activeCanvas = canvas3;
        }
        else
        {
            activeCanvas = canvas1;
        }

        // Activate new active canvas
        activeCanvas.gameObject.SetActive(true);
    }
}
