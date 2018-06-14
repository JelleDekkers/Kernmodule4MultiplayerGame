using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : CharacterControllerInput {

    Vector3 objectPos, dir;

    public override float Horizontal {
        get { return Input.GetAxis("Horizontal"); }
    }

    public override float Vertical {
        get { return Input.GetAxis("Vertical"); }
    }

    public override float Rotation {
        get {
            objectPos = Camera.main.WorldToScreenPoint(transform.position);
            dir = Input.mousePosition - objectPos;
            return Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        }
    }

    public override bool FireKeyPressed {
        get { return Input.GetMouseButtonDown(0); }
    }

    public override bool SecondaryKeyPressed {
        get { return Input.GetMouseButtonDown(1); }
    }

    public override bool FireKeyHeldDown {
        get { return Input.GetMouseButton(0); }
    }

    public override bool SecondaryKeyHeldDown {
        get { return Input.GetMouseButton(1); }
    }
}
