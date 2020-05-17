using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public float lookSensitivity = 1f;
    public float forwardSpeed = 1f;
    public float strafeSpeed = 1f;
    public float jumpHeight = 1f;

    private Vector3 lookDirection;
    private float rotationX;
    private float rotationY;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        lookDirection = Vector3.forward;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        MouseLook();
    }

    public Vector3 GetLookDirection()
    {
        return lookDirection;
    }

    public Vector3 GetForwardDirection()
    {
        return new Vector3(lookDirection.x, 0f, lookDirection.z).normalized;
    }

    public Vector3 GetStrafeDirection()
    {
        return new Vector3(lookDirection.z, 0f, -lookDirection.x).normalized;
    }

    private void MouseLook()
    {
        rotationX += Input.GetAxis("Mouse X") * lookSensitivity;
        rotationY += Input.GetAxis("Mouse Y") * lookSensitivity;
        rotationY = Mathf.Clamp(rotationY, -80f, 80f);
        Quaternion rotation = Quaternion.AngleAxis(rotationX, Vector3.up) * Quaternion.AngleAxis(rotationY, Vector3.left);
        lookDirection = (rotation * Vector3.forward).normalized;

        transform.rotation = Quaternion.LookRotation(GetForwardDirection(), Vector3.up);
    }

    private void Move()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float strafeInput = Input.GetAxis("Horizontal");
        Vector3 forwardVelocity = forwardInput * forwardSpeed * GetForwardDirection();
        Vector3 strafeVelocity = strafeInput * strafeSpeed * GetStrafeDirection();

        Vector3 verticalVelocity = rb.velocity.y * Vector3.up;
        if (Input.GetButtonDown("Jump"))
        {
            verticalVelocity = jumpHeight * Vector3.up;
        }

        rb.velocity = forwardVelocity + strafeVelocity + verticalVelocity;
    }
}
