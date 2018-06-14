using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Item : NetworkBehaviour {

    public int id;
    public abstract ItemData Data { get; }
    public Player owner;

    public abstract void Init(CharacterControllerInput input);
    public abstract void Tick();

    //private void Start() {
    //    transform.rotation = transform.parent.rotation;
    //    transform.position = Vector3.zero;
    //}

    [ClientRpc]
    public void RpcOwnershipSetup(int playerID) {
        owner = TurnManager.GetPlayer(playerID);
    }
}
