using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int totalTargetsCount = 10;
    [SerializeField] private int currentTargetsCount = 0;

    [Space, SerializeField] private TMP_Text scoreText;
    
    public UnityEvent onWin;

    private void Start()
    {
        UpdateUI();
    }

    private void IncrementScore()
    {
        currentTargetsCount++;
        UpdateUI();
        
        if (currentTargetsCount >= totalTargetsCount)
        {
            onWin.Invoke();
        }
    }

    private void UpdateUI()
    {
        scoreText.text = String.Format("Score: {0}/{1}", currentTargetsCount, totalTargetsCount);
    }
}
