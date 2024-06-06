using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BowController : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable bow;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private LineRenderer stringRenderer;
    [Space, SerializeField] private XRGrabInteractable stringAnchor;
    [Space, Range(0.0f, 1.0f), SerializeField] private float stringStretchMin = 0.14f;
    [Space, Range(0.0f, 1.0f), SerializeField] private float stringStretchMax = 0.3f;
    [SerializeField] private Vector3 arrowOffset;
    [Space, SerializeField] private AudioSource drawAudioSource;
    [SerializeField] private List<AudioClip> drawAudioClips = new List<AudioClip>();
    
    [Space] public UnityEvent onHitFruit;
    
    private Arrow arrow = null;
    private XRBaseController bowHand = null, stringHand = null;
    private Vector3 anchorPosition = new Vector3();

    private void Start()
    {
        bow.selectEntered.AddListener(PullBow);
        bow.selectExited.AddListener(ReleaseBow);

        stringAnchor.selectEntered.AddListener(PullString);
        stringAnchor.selectExited.AddListener(ReleaseString);
        
        Reload(0.0f);
    }

    private void Update()
    {
        if(stringHand != null)
        {
            anchorPosition = stringRenderer.transform.InverseTransformPoint(stringAnchor.transform.position);
            // Apply constraints to the string.
            anchorPosition.x = 0.0f;
            anchorPosition.y = 0.0f;
            anchorPosition.z = Mathf.Clamp(anchorPosition.z, -stringStretchMax, -stringStretchMin);

            stringRenderer.SetPosition(1, anchorPosition);
            arrow.transform.localPosition = anchorPosition + arrowOffset;

            float strength = Mathf.Clamp01((Mathf.Abs(anchorPosition.z) - stringStretchMin) / stringStretchMax);

            SetControllerHaptic(stringHand, strength * 0.6f);
            SetControllerHaptic(bowHand, strength * 0.1f);
        }
    }

    private void PullString(SelectEnterEventArgs arg0)
    {
        stringHand = arg0.interactorObject.transform.GetComponent<XRBaseController>();

        AudioClip clip = drawAudioClips[Random.Range(0, drawAudioClips.Count)];
        drawAudioSource.PlayOneShot(clip);
    }

    public void ReleaseString(SelectExitEventArgs arg0)
    {
        float strength = Mathf.Clamp01((Mathf.Abs(stringAnchor.transform.localPosition.z) - stringStretchMin) / stringStretchMax);
        Debug.Log("Throwing arrow with strength of " + strength.ToString());

        anchorPosition = Vector3.zero;
        stringRenderer.SetPosition(1, anchorPosition);
        stringAnchor.transform.localPosition = Vector3.zero;

        arrow.transform.parent = null;
        arrow.Release(strength);

        SetControllerHaptic(stringHand, 0.0f);
        stringHand = null;
        SetControllerHaptic(bowHand, 1.0f, 0.3f);

        Reload(0.2f);
    }

    private void PullBow(SelectEnterEventArgs arg0)
    {
        bowHand = arg0.interactorObject.transform.GetComponent<XRBaseController>();
    }

    private void ReleaseBow(SelectExitEventArgs arg0)
    {
        bowHand = null;
    }

    private void Reload(float delay)
    {
        StartCoroutine(ReloadCouroutine(delay));
    }

    private IEnumerator ReloadCouroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        GameObject go = Instantiate(arrowPrefab);
        arrow = go.GetComponent<Arrow>();
        arrow.OnCollide += OnArrowHitFruitEvent;
        
        arrow.transform.parent = stringAnchor.transform.parent;
        arrow.transform.localPosition = arrowOffset;
        arrow.transform.localRotation = Quaternion.identity;
    }

    private void OnArrowHitFruitEvent(Transform other)
    {
        Debug.Log("Arrow hit " + other.name);
        
        TargetAnimation anim = other.GetComponent<TargetAnimation>();
        anim?.TriggerDestroyAnimation();
        
        onHitFruit.Invoke();
    }

    private void SetControllerHaptic(XRBaseController controller, float intensity, float duration = 0.16f)
    {
        controller?.SendHapticImpulse(intensity, duration);
    }

}
