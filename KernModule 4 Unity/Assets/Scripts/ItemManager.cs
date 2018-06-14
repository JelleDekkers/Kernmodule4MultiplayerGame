using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "items Manager", menuName = "Items Manager", order = 0)]
public class ItemManager : ScriptableObjectSingleton<ItemManager> {

    public Object[] items;

    private const string FOLDER_PATH = "Items";

    public void UpdateList() {
        items = Resources.LoadAll(FOLDER_PATH, typeof(GameObject));
        for(int i = 0; i < items.Length; i++) {
            GameObject item = items[i] as GameObject;
            item.GetComponent<Item>().id = i;
        }
    }

    public static Item GetItembyID(int id) {
        return (Instance.items[id] as GameObject).GetComponent<Item>();
    }
}
