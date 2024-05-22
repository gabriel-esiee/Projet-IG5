using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float maxVelocity = 30.0f;
    [SerializeField] private float lifeTime = 10.0f;

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

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
    }

    public void Release(float strength)
    {
        if (released == false)
        {
            released = true;
            col.enabled = true;
            rb.isKinematic = false;
            rb.AddForce(transform.forward * maxVelocity * strength, ForceMode.Impulse);
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
            StickToSurface(other.collider.transform);
        }
    }
}
