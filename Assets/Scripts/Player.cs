using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
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

            float vx = dx * strafeSpeed;
            float vy = dz * moveSpeed;
            rb.velocity = new Vector3(vx, 0f, vy);
        }
    }
}
