using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Color Manager", menuName = "Player Color Manager", order = 0)]
public class PlayerColorManager : ScriptableObjectSingleton<PlayerColorManager> {

    public Color[] colors;

    public static Color GetColor(int id) {
        return Instance.colors[id % Instance.colors.Length];
    }
}
