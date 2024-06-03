using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float x = Mathf.Sin(Time.time / 2.0f) * 2.0f;
        Vector3 position = new Vector3(x, transform.position.y, transform.position.z);
        rb.MovePosition(position);
    }
}
