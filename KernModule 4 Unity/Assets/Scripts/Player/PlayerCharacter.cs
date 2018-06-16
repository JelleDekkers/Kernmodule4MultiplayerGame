using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCharacter : NetworkBehaviour, IDamageable {

    public Player Owner { get; private set; }
    public Inventory inventory;
    public Action onHit;

    public static Action<PlayerCharacter> OnLocalCharacterCreated;

    private void Start() {
        inventory = GetComponent<Inventory>();
        TurnManager.OnGameOver += DisableOnGameOver;
    }

    [ClientRpc]
    public void RpcInit(int id) {
        Owner = TurnManager.GetPlayer(id);
        transform.SetParent(Owner.transform);
        InitComponents();

        if(Owner.isLocalPlayer)
            OnLocalCharacterCreated.Invoke(this);
    }

    private void InitComponents() {
        IPlayerOwnedObject[] references = GetComponentsInChildren<IPlayerOwnedObject>();
        foreach (IPlayerOwnedObject r in references)
            r.InitOwnership(Owner);
    }

    public void Hit(DamageInfo info) {
        if (!isServer)
            return;

        if (onHit != null)
            onHit.Invoke();

        Debug.Log("sendin cmd from Owner " + Owner.id + " recipient " + info.OwnerID);
        CmdOnHitRecieved(Owner.id, info.OwnerID, info.ItemID);
    }

    [Command]
    private void CmdOnHitRecieved(int hitPlayerID, int recipientPlayerID, int itemID) {
        RpcOnPlayerHit(hitPlayerID, recipientPlayerID, itemID);
    }

    [ClientRpc]
    private void RpcOnPlayerHit(int hitPlayerID, int recipientPlayerID, int itemID) {
        Player playerHit = TurnManager.GetPlayer(hitPlayerID); 
        Player recipient = TurnManager.GetPlayer(recipientPlayerID);
        Item weapon = ItemManager.GetItembyID(itemID);

        string playerHitName = playerHit.name;
        if (playerHit.id == Owner.id)
            playerHitName = "You ";
        Debug.Log(playerHitName + " was hit by " + recipient.name + " with a " + weapon.Data.name);
        recipient.hits++;
    }

    private void DisableOnGameOver() {
        TurnManager.OnGameOver -= DisableOnGameOver;
        enabled = false;
    }
}
