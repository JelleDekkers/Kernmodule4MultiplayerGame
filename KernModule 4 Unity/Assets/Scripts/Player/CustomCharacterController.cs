using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomCharacterController : NetworkBehaviour {

    public FloatVariable movementSpeed;
    public FloatVariable rotationSpeed;
    public CharacterControllerInput input;

    private void Update() {
        transform.Translate(input.Horizontal * movementSpeed.runTimeValue * Time.deltaTime, 0, input.Vertical * movementSpeed.runTimeValue * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, input.Rotation, 0), rotationSpeed.runTimeValue * Time.deltaTime);
    }
}