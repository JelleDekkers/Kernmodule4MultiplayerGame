using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCharacter : NetworkBehaviour, IDamageable {

    public Player Owner { get; private set; }
    public float maxHealthPoints = 100;

    [SyncVar]
    public float healthPoints;

    public Inventory inventory;
    public Action onDeath;

    private void Start() {
        inventory = GetComponent<Inventory>();
    }

    [ClientRpc]
    public void RpcInit(int id) {
        Owner = TurnManager.GetPlayer(id);
        transform.SetParent(Owner.transform);
        InitComponents();
    }

    private void InitComponents() {
        IPlayerOwnedObject[] references = GetComponentsInChildren<IPlayerOwnedObject>();
        foreach (IPlayerOwnedObject r in references)
            r.InitOwnership(Owner);
    }

    public void Hit(DamageInfo info) {
        if (!isServer)
            return;

        CmdTakeDamage(info.Damage, info.OwnerID, info.ItemID);
    }

    [Command]
    private void CmdTakeDamage(float amount, int damageFromPlayerID, int itemID) {
        healthPoints -= amount;
        Player playerThatDidDamage = null;
        Item weapon = null;

        if (damageFromPlayerID > 0) {
            playerThatDidDamage = TurnManager.GetPlayer(damageFromPlayerID);
            weapon = ItemManager.GetItembyID(itemID);
        }

        if (healthPoints < 0) {
            CmdDie();
            if(playerThatDidDamage != null) {
                Debug.Log(Owner.id + " was killed by " + playerThatDidDamage.name + " with a " + weapon.Data.name);
                playerThatDidDamage.kills++;
            }
            healthPoints = 0;
        } 
    }

    [Command]
    private void CmdDie() {
        Debug.Log("DIE");
        if (onDeath != null)
            onDeath.Invoke();
        NetworkServer.Destroy(gameObject);
    }

    public void Heal(float amount) {
        healthPoints += amount;
        if (healthPoints > maxHealthPoints)
            healthPoints = maxHealthPoints;
    }
}
