using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent onTimerEnd;

    [SerializeField] private float timerDuration = 10.0f;
    
    private bool started = false;
    private float timer;

    private void Start()
    {
        StartTimer(timerDuration);
    }

    public void StartTimer(float duration)
    {
        if (duration <= 0.0f)
        {
            Debug.LogError("Timer duration should be greater than zero!");
        }

        timer = duration;
        started = true;
    }

    public void StopTimer()
    {
        started = false;
    }

    private void Update()
    {
        if (started && timer > 0.0f)
        {
            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                timer = 0.0f;
                onTimerEnd.Invoke();
            }
        }
    }
}
