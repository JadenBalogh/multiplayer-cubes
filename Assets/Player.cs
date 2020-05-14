using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(Random.Range(-4f, 4f), 0.5f, 0f);
    }
}
