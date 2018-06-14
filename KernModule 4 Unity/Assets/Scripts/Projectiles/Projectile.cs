using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Projectile : NetworkBehaviour {

    public Player player;
    public Item WeaponFiredFrom { get; private set; }

    public FloatVariable unFreezeTimeOnTurn;

    [ClientRpc]
	public void RpcInit(int playerID, int itemID) {
        player = TurnManager.GetPlayer(playerID);
        WeaponFiredFrom = ItemManager.GetItembyID(itemID);
    }

    public abstract void OnNewTurn(int playerID, int turnNr);
    public abstract void OnTurnEnded();
}
