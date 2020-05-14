using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float strafeSpeed = 1f;

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
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");

        float velocityX = dx * strafeSpeed * Time.deltaTime;
        float velocityZ = dz * moveSpeed * Time.deltaTime;
        Vector3 force = new Vector3(velocityX, 0f, velocityZ);

        rb.AddForce(force);
    }
}
