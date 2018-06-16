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
        InitExtraFunc(playerID, itemID);
    }

    // needs to be a seperate function because RPC functions can't be overridden
    public virtual void InitExtraFunc(int playerID, int itemID) { }

    public abstract void OnNewTurn(int playerID, int turnNr);
    public abstract void OnTurnEnded();
}
