using System;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    public UnityEvent onPress;
    public UnityEvent onRelease;
    
    [SerializeField] private GameObject button;
    [SerializeField] private Vector3 pressedPosition, releasedPosition;

    private AudioSource audioSource;
    private bool isPressed = false;
    
    private Transform hand;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        onRelease.AddListener(OnReleaseButton);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPressed == false)
        {
            button.transform.localPosition = pressedPosition;
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
            button.transform.localPosition = releasedPosition;
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
