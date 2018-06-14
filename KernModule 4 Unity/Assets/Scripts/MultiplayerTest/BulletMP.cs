using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMP : MonoBehaviour {

    private void Start() {
        Destroy(gameObject, 5);
    }

    void OnCollisionEnter(Collision collision) {
        var hit = collision.gameObject;
        var hitPlayer = hit.GetComponent<PlayerMP>();

        if (hitPlayer != null) {
            hit.GetComponent<PlayerMP>().TakeDamage(10);

            Destroy(gameObject);
        }
    }
}
