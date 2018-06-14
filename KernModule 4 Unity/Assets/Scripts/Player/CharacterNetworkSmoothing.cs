using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterNetworkSmoothing : NetworkBehaviour, IPlayerOwnedObject {

    [SyncVar]
    private Vector3 realPosition = Vector3.zero;
    [SyncVar]
    private Quaternion realRotation;

    public float updateIntervalPerFrame = 0.11f;
    public float lerpStrength = 0.1f;

    private float updateValue;

    public Player Owner {
        get; private set;
    }

    private void Start() {
        updateIntervalPerFrame = updateValue;
    }

    public void InitOwnership(Player player) {
        Owner = player;
        enabled = true;
    }

    void Update() {
        if (Owner.isLocalPlayer) {
            updateValue -= Time.deltaTime;

            if (updateValue < 0) {
                updateValue = updateIntervalPerFrame;
                CmdSyncTransform(transform.position, transform.rotation);
            }
        } else {
            transform.position = Vector3.Lerp(transform.position, realPosition, lerpStrength);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, lerpStrength);
        }
    }

    [Command]
    void CmdSyncTransform(Vector3 position, Quaternion rotation) {
        realPosition = position;
        realRotation = rotation;
    }
}