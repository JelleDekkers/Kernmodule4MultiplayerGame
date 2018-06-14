using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity))]
public class TurnManager : NetworkBehaviour {

    // singleton for testing purposes:
    private static TurnManager instance;
    public static TurnManager Instance { get { return instance; } }

    [SyncVar]
    public bool gameStarted;
    [SyncVar]
    public int currentTurn = -1;
    [SyncVar]
    public int currentTurnPlayerID = -1;

    public List<Player> players;
    public Player localPlayer;

    public FloatVariable secondsPerTurn;
    public GameEvent onNewTurnGameEvent;

    public static Action<int, int> onNewTurn;
    public static Action onTurnEnd;
    public static bool isMyTurn;
    public static Action OnGameOver;

    public int playerIdCounter;
    List<int> activePlayerIds = new List<int>();

    private NetworkIdentity networkIdentity;

    private void Awake() {
        instance = this;
        Player.OnNewPlayer += RegisterNewPlayer;
        networkIdentity = GetComponent<NetworkIdentity>();
    }

    private void Update() {
        if (isServer && Input.GetKeyDown(KeyCode.Space))
            EndGame();
    }

    // Needs it's own function because at RegisterNewPlayer() the player.LocalPlayer var hasn't been set yet 
    public void SetLocalPlayer(NetworkIdentity playerNetworkIdentity, int id) {
        localPlayer = players[id];
        if(playerNetworkIdentity.isServer)
            networkIdentity.AssignClientAuthority(playerNetworkIdentity.connectionToClient);
    }

    public static Player GetPlayer(int id) {
        return instance.players[id];
    }

    public int RegisterNewPlayer(Player player) {
        activePlayerIds.Add(playerIdCounter);
        players.Add(player);
        return playerIdCounter++;
    }

    public void RemovePlayer(Player player) {
        activePlayerIds.Clear();
        foreach (Player p in players)
            activePlayerIds.Add(p.id);

        int currentId = activePlayerIds[currentTurnPlayerID];
        if (activePlayerIds.Contains(currentId)) {
            currentTurnPlayerID = activePlayerIds.IndexOf(currentId);
        } else {
            currentTurnPlayerID = 0;
        }

        playerIdCounter--;
    }

    public static bool IsPlayerTurn(Player player, int currentTurnPlayerID) {
        return player.id == currentTurnPlayerID;
    }

    //private void RemoveDisconnectedPlayer(Player player) {
    //    players.Remove(player);
    //    RebuildPlayerIDs();
    //}

    //// server only
    //private void RebuildPlayerIDs() {
    //    for(int i = 0; i < players.Count; i++) {
    //        players[i].id = i;
    //    }
    //}

    /// <summary>
    /// Needs these parameters because syncvars are annoyingly synced after this function gets called
    /// </summary>
    /// <param name="newCurrentTurn"></param>
    /// <param name="newCurrentTurnPlayerID"></param>
    [ClientRpc]
    private void RpcNextTurn(int newCurrentTurn, int newCurrentTurnPlayerID) {
        EndTurn();
        isMyTurn = IsPlayerTurn(localPlayer, newCurrentTurnPlayerID);
        if (onNewTurn != null)
            onNewTurn.Invoke(newCurrentTurn, newCurrentTurnPlayerID);
        Debug.Log("TurnManagar: Next Turn, turn id: " + newCurrentTurn + " playerID's turn: " + newCurrentTurnPlayerID + " is my turn " + isMyTurn);
    }

    [Command]
    private void CmdStartNextTurn() {
        currentTurn++;
        currentTurnPlayerID++;
        if (currentTurnPlayerID > players.Count - 1)
            currentTurnPlayerID = 0;

        Debug.Log("Start next Turn " + currentTurn + " curTurnPlayerID:" + currentTurnPlayerID);
        RpcNextTurn(currentTurn, currentTurnPlayerID);
        if(networkIdentity.clientAuthorityOwner != null)
        networkIdentity.RemoveClientAuthority(networkIdentity.clientAuthorityOwner);
        networkIdentity.AssignClientAuthority(players[currentTurnPlayerID].connectionToClient);
    }

    private void StartGame() {
        SpawnPlayerCharacters();
        gameStarted = true;
        CmdStartNextTurn();
    }

    private void SpawnPlayerCharacters() {
        foreach (Player player in players)
            player.SpawnCharacterObject();
    }

    private IEnumerator TurnTimer() {
        float timer = secondsPerTurn.runTimeValue;
        while(timer > 0) {
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void EndTurn() {
        if (onTurnEnd != null)
            onTurnEnd.Invoke();
    }

    private void EndGame() {
        Debug.Log("end game");
        RpcEndGame();
    }

    [ClientRpc]
    private void RpcEndGame() {
        if(OnGameOver != null)
            OnGameOver.Invoke();
    }

    private void OnGUI() {
        if (!gameStarted) {
            if (isServer && GUI.Button(new Rect(10, 10, 200, 20), "Start Game"))
                StartGame();
        } else {
            if (isMyTurn) {
                if (GUI.Button(new Rect(10, 10, 200, 20), "End Turn"))
                    CmdStartNextTurn();
            }
        }
    }

    private void OnDestroy() {
        Player.OnNewPlayer += RegisterNewPlayer;
    }
}
