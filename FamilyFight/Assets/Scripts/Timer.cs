
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class of a timer.

public class Timer
{

    private bool    resetOnTargetReach,
        
                    stopped;

    // Current elapsed time.

    private float   elapsed_t,
        
    // Time to reach.

                    target_t;

    // Constructor.
    // param start_t    Starting time of the timer
    // param target_t   Time to reach

    public Timer(bool resetOnTargetReach, float start_t, float target_t)
    {
        this.resetOnTargetReach = resetOnTargetReach;
        this.elapsed_t = start_t;
        this.target_t = target_t;
    }

    // Constructor.
    // param target_t   Time to reach

    public Timer(bool resetOnTargetReach, float target_t) : this(resetOnTargetReach, 0f, target_t)
    {
    }

    // Constructor.
    // param target_t   Time to reach

    public Timer(float target_t) : this(true, 0f, target_t)
    {
    }

    // Method that returns the elapsed time.

    public float GetElapsedTime()
    {
        return elapsed_t;
    }

    public float GetTargetTime()
    {
        return target_t;
    }

    // Method that sets the target time.
    // param target_t Time to reach

    public void SetTargetTime(float target_t)
    {
        this.target_t = target_t;
    }

    // Method that resets the timer.

    public void Reset()
    {
        elapsed_t = 0f;
        Resume();
    }

    // Method that updates the timer and checks if the target time is reached.

    public bool UpdateAndCheck()
    {
        Update();
        return Check();
    }

    // Method that when the target time is reached resets the elapsed time and returns true.

    public bool Check()
    {
        if (elapsed_t >= target_t)
        {
            if (resetOnTargetReach)
                Reset();
            else
                Stop();

            return true;
        }
        else
            return false;
    }

    // Method that increases the elapsed time.

    public void Update()
    {
        if(!stopped)
            elapsed_t += Time.deltaTime;
    }

    public void Stop()
    {
        stopped = true;
    }

    public void Resume()
    {
        stopped = false;
    }

    public float RemainingTime()
    {
        return target_t - elapsed_t;
    }

}
