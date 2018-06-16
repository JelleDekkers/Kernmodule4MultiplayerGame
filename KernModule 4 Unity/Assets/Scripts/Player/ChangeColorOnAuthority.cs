using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChangeColorOnAuthority : NetworkBehaviour {

    public Color color = Color.red;

    public override void OnStartAuthority() {
        base.OnStartAuthority();
        GetComponent<Renderer>().material.color = color;
    }
}
