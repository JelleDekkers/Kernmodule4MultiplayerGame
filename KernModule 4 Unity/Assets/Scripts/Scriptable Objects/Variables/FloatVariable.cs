using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/" + "Float")]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver {

    [NonSerialized]
    public float runTimeValue;
    public float initialValue;  

    public void OnAfterDeserialize() {
        runTimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
