using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScorePanel : MonoBehaviour {

    public Text playerName, kills;

    public void Init(Player player) {
        playerName.text = player.name;
        kills.text = player.hits.ToString();
    }
}
