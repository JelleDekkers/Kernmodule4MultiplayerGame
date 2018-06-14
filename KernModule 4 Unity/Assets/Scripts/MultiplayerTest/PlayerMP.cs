using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMP : NetworkBehaviour {

    public const int maxHealth = 100;
    [SyncVar]
    public int health = maxHealth;

    public void TakeDamage(int amount) {
        if (!isServer)  
            return;

        health -= amount;
        if (health <= 0) {
            health = 0;
            Debug.Log("Dead!");
        }

        RpcRespawn();
    }

    [ClientRpc]
    void RpcRespawn() {
        if (isLocalPlayer) {
            transform.position = Vector3.zero;
        }
    }

    private void OnGUI() {
        if (!isClient)
            return;

        GUI.Label(new Rect(10, 200, 100, 20), "isServer " + isServer);
        GUI.Label(new Rect(10, 220, 100, 20), "HP " + health);
    }
}