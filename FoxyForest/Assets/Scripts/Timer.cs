using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Timer : MonoBehaviour
{
    public float initialTime = 10; // The initial time for the scene, set to 10 seconds
    public float timeRemaining; // A float to track the remaining time in the scene
    public bool timerIsRunning = false; // A boolean to track whether the timer is running
    public Image timerCircleImage; // Reference to the Image component for the timer circle

    void Start()
    {
        // Set the time remaining to the initial time
        timeRemaining = initialTime;

        // Start the timer when the scene starts
        timerIsRunning = true;
    }

    void Update()
    {
        // Check if the timer is running
        if (timerIsRunning)
        {
            // If there is still time remaining, decrease the time and update the display
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                // Update the circle fill amount
                timerCircleImage.fillAmount = timeRemaining / initialTime;
            }

            // If time has run out, stop the timer and load the next scene
            else
            {
                //Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                // Return to the main menu after time has run out, will later change to the next season/level
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
