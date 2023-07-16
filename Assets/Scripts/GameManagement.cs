using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject[] canvasTraveller;
    public GameObject[] canvasGuide;
    private int currentIndex = 0;

    private void Start()
    {
        //reset level index at start
        currentIndex = 0;
        
        //Disable all levels except first in the list
        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
            canvasGuide[i].SetActive(false);
            canvasTraveller[i].SetActive(false);
        }
    }

    private void Update()
    {
        //// Check if the space bar is pressed (remove once added to other level conditions)
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // Call the function to enable the next level
        //    goToNextLevel();
        //}
    }

    public void goToNextLevel()
    {
        currentIndex++;

        if (currentIndex < levels.Length)
        {
            // Disable the previous GameObject (if any)
            if (currentIndex > 0)
            {
                levels[currentIndex - 1].SetActive(false);
                canvasGuide[currentIndex - 1].SetActive(false);
                canvasTraveller[currentIndex - 1].SetActive(false);
            }

            // Enable the next GameObject in the list
            levels[currentIndex].SetActive(true);
            canvasGuide[currentIndex].SetActive(true);
            canvasTraveller[currentIndex].SetActive(true);
        }
    }
}
