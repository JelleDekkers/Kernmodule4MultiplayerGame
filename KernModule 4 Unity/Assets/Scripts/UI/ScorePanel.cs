using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScorePanel : MonoBehaviour {

    public Text playerName, kills, deaths;

    public void Init(Player player) {
        playerName.text = player.name;
        kills.text = player.kills.ToString();
        deaths.text = player.deaths.ToString();
    }
}
