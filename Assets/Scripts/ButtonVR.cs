using System;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    public UnityEvent onPress;
    public UnityEvent onRelease;
    
    [SerializeField] private GameObject button;

    private AudioSource audioSource;
    private bool isPressed = false;
    
    private Transform hand;
    private Vector3 defaultPosition;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        onRelease.AddListener(OnReleaseButton);
        defaultPosition = button.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPressed == false)
        {
            button.transform.localPosition = new Vector3(0.0f, 0.223f, 0.0f);
            hand = other.transform;
            onPress.Invoke();
            audioSource.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == hand && isPressed == true)
        {
            button.transform.localPosition = defaultPosition;
            hand = null;
            onRelease.Invoke();
            isPressed = false;
        }
    }

    private void OnReleaseButton()
    {
        Debug.Log("Button pressed!");
    }
}
