using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnSelectDifficulty(int difficulty)
    {
        if (difficulty == 1)
        {
            StartCoroutine(LoadSceneDelayed(1, 1.0f));
        }
        else if (difficulty == 2)
        {
            StartCoroutine(LoadSceneDelayed(2, 1.0f));
        }
        else if (difficulty == 3)
        {
            StartCoroutine(LoadSceneDelayed(3, 1.0f));
        }
        else
        {
            Debug.LogError("Unknown difficulty level: " + difficulty + ".");
        }
    }

    private IEnumerator LoadSceneDelayed(int buildindex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(buildindex);
    }
}
