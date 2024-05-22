using UnityEngine;

public class BowController : MonoBehaviour
{
    [Range(0.0f, 1.0f), SerializeField] private float pull;

    [SerializeField] private Arrow arrow;
    [SerializeField] private LineRenderer bowString;
    [SerializeField] private Transform stringAnchor, bowAnchor;
    [Space, SerializeField] private Vector2 stringPositionRange;
    
    private void Start()
    {
        arrow.transform.parent = stringAnchor;
        arrow.transform.localPosition = Vector3.zero;
        arrow.transform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        float z = stringPositionRange.x + (stringPositionRange.y * pull);
        bowString.SetPosition(1, new Vector3(0.0f, 0.0f, z));
        stringAnchor.localPosition = bowString.GetPosition(1);
    }
    
    private void OnGUI()
    {
        if (GUILayout.Button("Release"))
        {
            arrow.transform.parent = null;
            arrow.Release(pull);
        }
    }
}
