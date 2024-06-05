using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timerDuration = 10.0f;

    [Space, SerializeField] private TMP_Text timerText;
    
    [Space] public UnityEvent onTimerEnd;

    private bool started = false;
    private float timer;

    public void StartTimer()
    {
        if (timerDuration <= 0.0f)
        {
            Debug.LogError("Timer duration should be greater than zero!");
        }

        timer = timerDuration;
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
            
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        TimeSpan span = TimeSpan.FromSeconds(timer);
        timerText.text = String.Format("Timer: {0}", span.ToString(@"mm\:ss"));
    }
}
