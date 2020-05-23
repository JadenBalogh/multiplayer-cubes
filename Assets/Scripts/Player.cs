using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public Camera playerCamera;
    public float lookSensitivity = 1f;
    public float forwardSpeed = 1f;
    public float strafeSpeed = 1f;
    public float jumpHeight = 1f;
    public float groundAngle = 30f;
    public LayerMask groundLayermask;

    private Vector3 lookDirection;
    private float rotationX;
    private float rotationY;
    private bool grounded;
    private Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start() {
        if (!hasAuthority) {
            playerCamera.enabled = false;
            return;
        }
        
        lookDirection = Vector3.forward;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (!hasAuthority) {
            return;
        }

        Move();
    }

    void FixedUpdate() {
        if (!hasAuthority) {
            return;
        }

        MouseLook();
    }

    void OnCollisionEnter(Collision col) {
        Debug.Log("Yeet");
        bool isGroundLayer = ((1 << col.collider.gameObject.layer) & groundLayermask) != 0;
        if (isGroundLayer) {
            Vector3 normal = col.GetContact(0).normal;
            float surfaceAngle = Vector3.Angle(Vector3.up, normal);

            // Debug.Log(surfaceAngle);

            if (surfaceAngle < groundAngle) {
                grounded = true;
            }
        }
    }

    void OnCollisionExit(Collision col) {
        Debug.Log("Yote");
        bool isGroundLayer = ((1 << col.collider.gameObject.layer) & groundLayermask) != 0;
        if (isGroundLayer) {
            grounded = false;
        }
    }

    public Vector3 GetLookDirection() {
        return lookDirection;
    }

    public Vector3 GetForwardDirection() {
        return new Vector3(lookDirection.x, 0f, lookDirection.z).normalized;
    }

    public Vector3 GetStrafeDirection() {
        return new Vector3(lookDirection.z, 0f, -lookDirection.x).normalized;
    }

    private void MouseLook() {
        rotationX += Input.GetAxis("Mouse X") * lookSensitivity;
        rotationY += Input.GetAxis("Mouse Y") * lookSensitivity;
        rotationY = Mathf.Clamp(rotationY, -80f, 80f);
        Quaternion rotation = Quaternion.AngleAxis(rotationX, Vector3.up) * Quaternion.AngleAxis(rotationY, Vector3.left);
        lookDirection = (rotation * Vector3.forward).normalized;

        transform.rotation = Quaternion.LookRotation(GetForwardDirection(), Vector3.up);
    }

    private void Move() {
        // Get keyboard input
        float forwardInput = Input.GetAxis("Vertical");
        float strafeInput = Input.GetAxis("Horizontal");
        Vector3 forwardVelocity = forwardInput * forwardSpeed * GetForwardDirection();
        Vector3 strafeVelocity = strafeInput * strafeSpeed * GetStrafeDirection();

        // Grounded spherecast check
        // Ray ray = new Ray(transform.position, Vector3.down);
        // bool grounded = Physics.SphereCast(ray, 0.5f, groundedDistance, groundLayermask);

        // Vector3 verticalVelocity = rb.velocity.y * Vector3.up;
        if (Input.GetButtonDown("Jump") && grounded) {
            // verticalVelocity = jumpHeight * Vector3.up;
            // rb.AddForce(jumpHeight * Vector3.up, ForceMode.Impulse);
            rb.velocity = jumpHeight * Vector3.up;
        }

        // rb.velocity = forwardVelocity + strafeVelocity + verticalVelocity;
        transform.position += (forwardVelocity + strafeVelocity) * Time.deltaTime;
    }
}
