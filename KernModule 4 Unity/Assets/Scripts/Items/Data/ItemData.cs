using System;
using UnityEngine;

public abstract class ItemData : ScriptableObject {

    public new string name;

    public ItemData Clone() {
        return MemberwiseClone() as ItemData;
    }
}
