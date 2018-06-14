using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelManager : MonoBehaviour {

    public LayoutGroup panelGroup;
    public ScorePanel panelPrefab;

    private void Start() {
        TurnManager.OnGameOver += FillPlayerList;
    }

    private void FillPlayerList() {
        GetComponent<Canvas>().enabled = true;
        foreach (Player player in TurnManager.Instance.players) {
            ScorePanel panel = Instantiate(panelPrefab, panelGroup.transform) as ScorePanel;
            panel.Init(player);
        }
    }
}
