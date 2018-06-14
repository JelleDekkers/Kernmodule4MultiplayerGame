using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemManager))]
public class ItemsManagerEditor : Editor {

    ItemManager obj;

    private void OnEnable() {
        obj = target as ItemManager;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("Auto Update List")) {
            obj.UpdateList();
        }
    }
}
