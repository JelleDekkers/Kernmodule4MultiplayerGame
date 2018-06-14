using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour {

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        GizmosExtensions.DrawArrow(transform.position, transform.forward * 0.3f);
    }
}
