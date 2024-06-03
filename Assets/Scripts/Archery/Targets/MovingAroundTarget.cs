using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingAroundTarget : MonoBehaviour
{
    [SerializeField] private MovingAxe axe = MovingAxe.X;
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    
    private Vector3 origin;
    private Rigidbody rb;

    private void Awake()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        float cos = Mathf.Cos(Time.time * speed) * radius;
        float sin = Mathf.Sin(Time.time * speed) * radius;
        
        Vector3 position = Vector3.zero;
        if (axe == MovingAxe.X)
        {
            position.y = sin;
            position.z = cos;
        }
        else if (axe == MovingAxe.Y)
        {
            position.x = cos;
            position.z = sin;
        }
        else if (axe == MovingAxe.Z)
        {
            position.x = cos;
            position.y = sin;
        }
        
        rb.MovePosition(origin + position);
    }
}
