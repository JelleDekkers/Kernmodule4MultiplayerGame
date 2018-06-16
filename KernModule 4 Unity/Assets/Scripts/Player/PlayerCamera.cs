using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour {

    public PlayerCharacter target;
    public Vector3 pos;

    private new Camera camera;

	private void Start () {
        camera = GetComponent<Camera>();
        PlayerCharacter.OnLocalCharacterCreated += SetTarget;
        enabled = false;
	}

    private void SetTarget(PlayerCharacter character) {
        target = character;
        enabled = true;
        camera.enabled = true;
        //character.onDeath += StopFollowing;
        PlayerCharacter.OnLocalCharacterCreated -= SetTarget;
    }

    private void StopFollowing() {
        target = null;
        enabled = false;
        camera.enabled = false;
        PlayerCharacter.OnLocalCharacterCreated += SetTarget;
    }

    private void Update() {
        pos = target.transform.position;
        pos.y = transform.position.y;
        transform.position = pos;
    }

    private void OnDestroy() {
        PlayerCharacter.OnLocalCharacterCreated -= SetTarget;
    }
}
