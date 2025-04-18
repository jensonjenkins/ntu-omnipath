using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTimer : MonoBehaviour
{
    // Public variables
    public bool isTimerRunning = false; // Controls whether the timer is active
    public float currentTime = 0f; // Stores the current time value

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime;
        }
    }
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = 0f;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }
}
