using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColorHandler : MonoBehaviour, IPlayerOwnedObject {

    public Player Owner { get; private set; }

    public void InitOwnership(Player player) {
        Owner = player;
        AssignColor();
    }

    private void AssignColor() {
        GetComponent<Renderer>().material.color = Owner.color;
    }
}
