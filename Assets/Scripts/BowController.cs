using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowController : MonoBehaviour
{
    [SerializeField] private Transform bow;
    [SerializeField] private Arrow arrow;
    [SerializeField] private LineRenderer stringRenderer;
    [SerializeField] private XRGrabInteractable stringAnchor;
    [Space, Range(0.0f, 1.0f), SerializeField] private float stringStretchLimit = 0.3f;
    [SerializeField] private Vector3 arrowOffset;

    private Transform hand = null;
    public Vector3 anchorPosition = new Vector3();
    
    private void Start()
    {
        stringAnchor.selectEntered.AddListener(PullString);
        stringAnchor.selectExited.AddListener(ReleaseString);
        Reload();
    }
    
    private void Update()
    {
        if (hand != null) // If the hand hold the string, the string should follow.
        {
            Vector3 handLocalPosition = stringAnchor.transform.parent.InverseTransformPoint(hand.position);
            float z = Mathf.Clamp(handLocalPosition.z, -stringStretchLimit, 0.0f); // Apply constraints to the string.
            anchorPosition = new Vector3(0.0f, 0.0f, z);
        }
        
        stringRenderer.SetPosition(1, anchorPosition);
        arrow.transform.localPosition = anchorPosition + arrowOffset;
    }

    public void Reload()
    {
        arrow.transform.parent = stringAnchor.transform.parent;
        arrow.transform.localPosition = arrowOffset;
        arrow.transform.localRotation = Quaternion.identity;
        // TODO : Reset arrow's state.
    }

    private void PullString(SelectEnterEventArgs arg0)
    {
        hand = arg0.interactorObject.transform;
        Debug.Log("Pulling bow string.");
    }
    
    private void ReleaseString(SelectExitEventArgs arg0)
    {
        hand = null;
        float strength = Mathf.Clamp01(Mathf.Abs(stringAnchor.transform.localPosition.z) / stringStretchLimit);
        anchorPosition = Vector3.zero;
        Debug.Log("Throwing arrow with strength of " + strength.ToString());
        arrow.Release(strength);
    }
}
