using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControllerMP : NetworkBehaviour {

    public float speed = 0.1f;
    public GameObject bulletPrefab;

    public override void OnStartLocalPlayer() {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void Update() {
        if (!isLocalPlayer)
            return;

        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;

        transform.Translate(x, 0, z);

        if (Input.GetKeyDown(KeyCode.Space)) {
            CmdFire();
        }
    }

    [Command]
    private void CmdFire() {
        // create the bullet object from the bullet prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            transform.position - transform.forward,
            Quaternion.identity);

        // make the bullet move away in front of the player
        bullet.GetComponent<Rigidbody>().velocity = -transform.forward * 4;

        NetworkServer.Spawn(bullet);

        // make bullet disappear after 2 seconds
        Destroy(bullet, 2.0f);
    }
}