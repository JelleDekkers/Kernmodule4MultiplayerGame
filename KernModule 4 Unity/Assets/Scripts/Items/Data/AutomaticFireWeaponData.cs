using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/" + "Automatic Fire")]
public class AutomaticFireWeaponData : SingleFireWeaponData {

    public float fireRatePerMinute;
    public float coolDownBetweenShots;
    private bool canFire;
}
