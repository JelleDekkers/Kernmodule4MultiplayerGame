using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupCollector : MonoBehaviour, IPlayerOwnedObject {

    public Player Owner { get; private set; }

    public void InitOwnership(Player player) {
        Owner = player;
    }

    private void OnTriggerEnter(Collider other) {
        IPickupable pickup = other.gameObject.GetInterface<IPickupable>();
        if (pickup != null) 
            pickup.Pickup(Owner);
    }
}
