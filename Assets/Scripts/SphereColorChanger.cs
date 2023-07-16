using UnityEngine;

public class SphereColorChanger : MonoBehaviour
{
    public float colorChangeInterval = 60f / 7f; // Interval in seconds (7 times per minute)
    public Renderer sphereRenderer;

    public Material restingMaterial;
    public Material targetMaterial;
    private Material currentMaterial;

    private void Start()
    {
        // Set initial material
        currentMaterial = restingMaterial;
        sphereRenderer.material = currentMaterial;

        // Start the color change coroutine
        StartCoroutine(ColorChangeCoroutine());
    }

    private void SetMaterial(Material material)
    {
        sphereRenderer.material = material;
    }

    private System.Collections.IEnumerator ColorChangeCoroutine()
    {
        int colorChangeCount = 0; // keep track of the number of color changes that have occurred. It starts at 0

        while (colorChangeCount < 7) //This loop runs until the number of color changes reaches 7
        {
            SetMaterial(targetMaterial);
            yield return new WaitForSeconds(0.9f);  //This line pauses the execution of the coroutine for 900 ms. During this time, the sphere is displaying green.

            SetMaterial(restingMaterial);
            yield return new WaitForSeconds(colorChangeInterval - 0.9f); // It subtracts the 0.5 seconds to allow the sphere to display the red color for the remaining time.

            colorChangeCount++; // incrememt indicating that a color change has occurred
        }
    }
}
