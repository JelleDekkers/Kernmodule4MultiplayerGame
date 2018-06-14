using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo {

    public int OwnerID { get; private set; }
    public int ItemID { get; private set; }
    public float Damage { get; private set; }

    public DamageInfo(int playerID, int itemID, float damage) {
        OwnerID = playerID;
        ItemID = itemID;
        Damage = damage;
    }

}
