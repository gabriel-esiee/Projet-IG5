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
            //button.transform.position = pressedPosition;
            button.SetActive(false);
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
            //button.transform.position = releasedPosition;
            button.SetActive(true);
            hand = null;
            onRelease.Invoke();
        }
    }

    private void OnReleaseButton()
    {
        Debug.Log("Button pressed!");
    }
}
