using UnityEngine;

public class DebugBowController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject arrowPrefab;

    private Arrow arrow;
    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && arrow == null)
        {
            GameObject go = Instantiate(arrowPrefab, camera.transform.position - new Vector3(0.0f, 0.05f, 0.0f), camera.transform.rotation);
            go.transform.parent = camera.transform;
            arrow = go.GetComponent<Arrow>();
        }

        if (Input.GetButtonUp("Fire1") && arrow != null)
        {
            arrow.transform.parent = null;
            arrow.Release(1.0f);
            arrow = null;
        }
    }
}
