using UnityEngine;

public class Arrow : MonoBehaviour
{
    public delegate void CollideAction(Transform other);
    public event CollideAction OnCollide;
    
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
    
    public void Release(float strength)
    {
        if (released == false)
        {
            released = true;
            
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
        if (released == true && other.transform.CompareTag("Target"))
        {
            StickToSurface(other.collider.transform);
            
            if (OnCollide != null)
                OnCollide(other.transform);
        }
    }
}
