using System;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager {

    public override void OnServerDisconnect(NetworkConnection connection) {
        base.OnServerDisconnect(connection);
        TurnManager.OnClientDisconnect();
    }
}
