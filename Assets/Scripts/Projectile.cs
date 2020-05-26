using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter(Collider col) {
        Player player;
        if (col.TryGetComponent<Player>(out player)) {
            Debug.Log("Hit!");
            player.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
