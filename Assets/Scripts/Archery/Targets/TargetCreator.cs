using System.Collections.Generic;
using UnityEngine;

public class TargetCreator : MonoBehaviour
{
    [SerializeField] private int targetsCount = 10;
    [SerializeField] private List<Transform> targets = new List<Transform>();
    
    private void Start()
    {
        Clear();
    }

    public void ActivateRandomTargets(int count)
    {
        if (count > targets.Count)
        {
            Debug.LogError("Not enough targets in the scene to activate " + count.ToString() + " of them.");
            count = targets.Count;
        }
        
        // Select randomly x indices.
        List<int> indices = new List<int>();
        while (indices.Count < count)
        {
            int i = Random.Range(0, targets.Count);
            if (indices.Contains(i) == false)
            {
                indices.Add(i);
            }
        }

        // Activate targets at selected indices.
        for (int i = 0; i < targets.Count; i++)
        {
            if (indices.Contains(i) == true)
            {
                targets[i].gameObject.SetActive(true);
            }
            else
            {
                targets[i].gameObject.SetActive(false);
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].gameObject.SetActive(false);
        }
    }
}
