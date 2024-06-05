using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Arrow : MonoBehaviour
{
    public delegate void CollideAction(Transform other);
    public event CollideAction OnCollide;
    
    [SerializeField] private float maxVelocity = 30.0f;
    [SerializeField] private float lifeTime = 10.0f;
    [SerializeField] private List<VisualEffect> vfx;
    public AudioSource hitAudioSource;
    public AudioClip[] fruitHitAudioClips;
    public AudioClip[] rockHitAudioClips;
    public AudioClip[] treeHitAudioClips;
    public AudioClip[] terrainHitAudioClips;
    public AudioClip[] splashHitAudioClips;
    public AudioSource whooshAudioSource;
    public AudioClip[] whooshAudioClips;
    public AudioSource wobbleAudioSource;
    public AudioClip[] wobbleAudioClips;
    public AudioSource fruitUpAudioSource;
    [SerializeField] private float pitchMin = 0.7f;
    [SerializeField] private float pitchMax = 1.3f;
    [SerializeField] private float volumeMin = 0.8f;
    [SerializeField] private float volumeMax = 1.0f;

    private Rigidbody rb;
    private Collider col;
    private bool released = false;
    
    private void Awake()
    {
        // Rigidbody.
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Arrow GameObject has no Rigidbody!");
        }
        else
        {
            rb.isKinematic = true;
        }

        // Collider.
        col = GetComponent<Collider>();
        
        if (col == null)
        {
            Debug.LogError("Arrow GameObject has no Collider!");
        }
        else
        {
            col.enabled = false;
        }
    }
    
    public void Release(float strength)
    {
        if (released == false)
        {
            released = true;

            PlayRandomWhooshSound();

            col.enabled = true;
            rb.isKinematic = false;
            transform.parent = null;
            rb.AddForce(transform.forward * maxVelocity * strength, ForceMode.Impulse);
            Destroy(gameObject, lifeTime);
        }
    }

    private void StickToSurface(Transform surface)
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        col.isTrigger = true;
        transform.parent = surface.transform;
    }

    private void OnCollisionEnter(Collision other)
    {

        if (released == true)
        {
            // Contact with fruits
            if (other.transform.CompareTag("Target"))
            {
                // count as collected
                if (OnCollide != null)
                    OnCollide(other.transform);

                StickToSurface(other.collider.transform);
                PlayRandomHitSound(0);
                fruitUpAudioSource.Play();
            }

            // Contact with rocks
            else if (other.transform.CompareTag("Rock"))
            {
                PlayRandomHitSound(1);
            }

            // Contact with trees
            else if (other.transform.CompareTag("Tree"))
            {
                StickToSurface(other.collider.transform);
                PlayRandomHitSound(2);
                PlayRandomWobbleSound();
            }

            // Contact with terrain
            else if (other.transform.CompareTag("Terrain"))
            {
                StickToSurface(other.collider.transform);
                PlayRandomHitSound(3);
                PlayRandomWobbleSound();
            }

            // Contact with terrain
            else if (other.transform.CompareTag("Water"))
            {
                Debug.Log("IMPACT WATER");
                PlayRandomHitSound(4);
            }

            foreach (VisualEffect elem in vfx)
            {
                elem.gameObject.SetActive(true);
                elem.transform.SetParent(null);
                Destroy(elem.gameObject, lifeTime);
            }


            
        }
    }

    public void PlayRandomHitSound(int i)
    {
        int randomIndex = 0;
        AudioClip[] selectedClips = null;

        // fruits
        if (i == 0)
        {
            selectedClips = fruitHitAudioClips;
        }
        // rocks
        else if (i == 1)
        {
            selectedClips = rockHitAudioClips;
        }
        // trees
        else if (i == 2)
        {
            selectedClips = treeHitAudioClips;
        }
        // terrain
        else if (i == 3)
        {
            selectedClips = terrainHitAudioClips;
        }
        // water
        else if (i == 4)
        {
            selectedClips = splashHitAudioClips;
            Debug.Log("PLAYED WATER");
        }

        if (selectedClips != null && selectedClips.Length > 0)
        {
            randomIndex = Random.Range(0, selectedClips.Length);
            hitAudioSource.clip = selectedClips[randomIndex];

            // Set random pitch and volume
            hitAudioSource.pitch = Random.Range(pitchMin, pitchMax);
            hitAudioSource.volume = Random.Range(volumeMin, volumeMax);

            hitAudioSource.Play();
        }
    }

    public void PlayRandomWhooshSound()
    {
        int randomIndex = Random.Range(0, whooshAudioClips.Length);

        whooshAudioSource.clip = whooshAudioClips[randomIndex];
        whooshAudioSource.Play();
    }

    public void PlayRandomWobbleSound()
    {
        int randomIndex = Random.Range(0, wobbleAudioClips.Length);

        wobbleAudioSource.clip = wobbleAudioClips[randomIndex];
        wobbleAudioSource.Play();
    }
}
