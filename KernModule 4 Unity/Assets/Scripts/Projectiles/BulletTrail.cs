using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour {

    public TrailRenderer trail;

    public void Init(Color color) {
        trail.startColor = color;
    }
}
