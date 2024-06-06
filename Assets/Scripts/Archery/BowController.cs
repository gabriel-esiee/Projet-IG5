using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BowController : MonoBehaviour
{
    [SerializeField] private Transform bow;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private LineRenderer stringRenderer;
    [SerializeField] private XRGrabInteractable stringAnchor;
    [Space, Range(0.0f, 1.0f), SerializeField] private float stringStretchMin = 0.14f;
    [Space, Range(0.0f, 1.0f), SerializeField] private float stringStretchMax = 0.3f;
    [SerializeField] private Vector3 arrowOffset;
    [Space, SerializeField] private AudioSource drawAudioSource;
    [SerializeField] private List<AudioClip> drawAudioClips = new List<AudioClip>();
    
    [Space] public UnityEvent onHitFruit;
    
    private Arrow arrow;
    private Transform hand = null;
    private Vector3 anchorPosition = new Vector3();

    private void Start()
    {
        stringAnchor.selectEntered.AddListener(PullString);
        stringAnchor.selectExited.AddListener(ReleaseString);
        
        Reload(0.0f);
    }
    
    private void Update()
    {
        if(hand != null)
        {
            anchorPosition = stringRenderer.transform.InverseTransformPoint(stringAnchor.transform.position);
            // Apply constraints to the string.
            anchorPosition.x = 0.0f;
            anchorPosition.y = 0.0f;
            anchorPosition.z = Mathf.Clamp(anchorPosition.z, -stringStretchMax, -stringStretchMin);

            stringRenderer.SetPosition(1, anchorPosition);
            arrow.transform.localPosition = anchorPosition + arrowOffset;
        }
    }
    
    public void ReleaseString(SelectExitEventArgs arg0)
    {
        hand = null;
        
        float strength = Mathf.Clamp01((Mathf.Abs(stringAnchor.transform.localPosition.z) - stringStretchMin) / stringStretchMax);
        Debug.Log("Throwing arrow with strength of " + strength.ToString());

        anchorPosition = Vector3.zero;
        stringRenderer.SetPosition(1, anchorPosition);
        stringAnchor.transform.localPosition = Vector3.zero;

        arrow.transform.parent = null;
        arrow.Release(strength);
        
        Reload(1.0f);
    }
    
    private void PullString(SelectEnterEventArgs arg0)
    {
        Debug.Log("Pulling bow string.");
        hand = arg0.interactorObject.transform;

        AudioClip clip = drawAudioClips[Random.Range(0, drawAudioClips.Count)];
        drawAudioSource.PlayOneShot(clip);
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
        arrow.OnCollide += OnArrowHitFruit;
        
        arrow.transform.parent = stringAnchor.transform.parent;
        arrow.transform.localPosition = arrowOffset;
        arrow.transform.localRotation = Quaternion.identity;
    }

    private void OnArrowHitFruit(Transform other)
    {
        Debug.Log("Arrow hit " + other.name);
        
        TargetAnimation anim = other.GetComponent<TargetAnimation>();
        anim?.TriggerDestroyAnimation();
        
        onHitFruit.Invoke();
    }
    
}
