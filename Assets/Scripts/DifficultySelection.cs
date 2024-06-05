using System;
using System.Collections;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    [Serializable]
    private class DifficultySettings
    {
        public int easyTargetsCount = 0;
        public int normalTargetsCount = 0;
        public int hardTargetsCount = 0;
    }

    [SerializeField] private GameObject difficultySelectionParent;
    [SerializeField] private Timer timer;
    [SerializeField] private Animator canvasAnimator;
    [SerializeField] private TargetCreator easyTargets, normalTargets, hardTargets;
    [SerializeField] private DifficultySettings easySettings, normalSettings, hardSettings;
    
    public void OnSelectDifficulty(int difficulty)
    {
        if (difficulty == 1)
        {
            StartCoroutine(DelayedGameStart(3.0f, easySettings));
        }
        else if (difficulty == 3)
        {
            StartCoroutine(DelayedGameStart(3.0f, hardSettings));
        }
        else
        {
            StartCoroutine(DelayedGameStart(3.0f, normalSettings));
        }
    }

    public void OnGameEnd()
    {
        easyTargets?.Clear();
        normalTargets?.Clear();
        hardTargets?.Clear();
    }

    private IEnumerator DelayedGameStart(float delay, DifficultySettings settings)
    {
        yield return new WaitForSeconds(1.5f);
        
        difficultySelectionParent.SetActive(false);
        canvasAnimator.SetTrigger("Start");
        
        yield return new WaitForSeconds(delay);
        
        easyTargets?.ActivateRandomTargets(settings.easyTargetsCount);
        normalTargets?.ActivateRandomTargets(settings.normalTargetsCount);
        hardTargets?.ActivateRandomTargets(settings.hardTargetsCount);
        timer.StartTimer();
    }
}
