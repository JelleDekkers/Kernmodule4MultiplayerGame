using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour, IPickupable {

    public ItemData weapon;

    public Vector3 rotateDirection;
    public float rotateSpeed;

    public void Pickup(Player player) {
        //player.character.inventory.AddWeapon(weapon.Clone());
        Destroy(gameObject);
    }

    private void Update() {
        transform.Rotate(rotateDirection * rotateSpeed * Time.deltaTime);
    }
}
