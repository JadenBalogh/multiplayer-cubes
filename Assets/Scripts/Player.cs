using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float lookSensitivity = 1f;
    public float forwardSpeed = 1f;
    public float strafeSpeed = 1f;
    public Vector3 lookDirection;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        transform.position = new Vector3(Random.Range(-4f, 4f), 0.5f, 0f);
    }

    void FixedUpdate()
    {
        float rotationX = Input.GetAxis("Mouse X") * lookSensitivity;
        float rotationY = Input.GetAxis("Mouse Y") * lookSensitivity;
        Quaternion rotation = Quaternion.AngleAxis(rotationX, Vector3.up) * Quaternion.AngleAxis(rotationY, Vector3.right);
        lookDirection = rotation * lookDirection;

        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");

        float vx = dx * strafeSpeed;
        float vy = dz * forwardSpeed;
        rb.velocity = new Vector3(vx, 0f, vy);
    }
}
