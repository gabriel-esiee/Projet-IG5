using UnityEngine;

public enum MovingAxe
{
    X,
    Y,
    Z
};

[RequireComponent(typeof(Rigidbody))]
public class MovingTraget : MonoBehaviour
{
    [SerializeField] private MovingAxe axe = MovingAxe.X;
    [SerializeField] private float magnitude = 2.0f;

    private Vector3 origin;
    private Rigidbody rb;

    private void Awake()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float v = Mathf.Sin(Time.time) * magnitude;
        
        Vector3 position = Vector3.zero;
        if (axe == MovingAxe.X)
        {
            position.x = v;
        }
        else if (axe == MovingAxe.Y)
        {
            position.y = v;
        }
        else if (axe == MovingAxe.Z)
        {
            position.z = v;
        }
        
        rb.MovePosition(origin + position);
    }
}
