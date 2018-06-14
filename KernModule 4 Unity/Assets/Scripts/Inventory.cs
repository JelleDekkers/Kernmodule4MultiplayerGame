using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour, IPlayerOwnedObject {

    public Player Owner { get; private set; }

    [SyncVar] 
    public int equippedWeaponID = -1;
    public Item equippedItem;
    public CharacterControllerInput input;

    // TODO: better weapon management:
    public Item[] allWeapons;

    [SerializeField]
    private Transform weaponPivot;

    public void InitOwnership(Player player) {
        Owner = player;

        if (hasAuthority)
            CmdSpawnItem(0);
        enabled = false;
    }

    private int GetWeaponIDFromList(Item weapon) {
        for(int i = 0; i < allWeapons.Length; i++) {
            if(equippedItem == allWeapons[i])
                return i;
        }
        Debug.LogWarning("No corresponding index found for weapon: " + equippedItem.name);
        return -1;
    }

    [Command]
    private void CmdSpawnItem(int id) {
        equippedItem = Instantiate(allWeapons[id], weaponPivot);
        equippedItem.transform.SetParent(weaponPivot);
        NetworkServer.SpawnWithClientAuthority(equippedItem.gameObject, Owner.connectionToClient);
        RpcSetWeaponParent(equippedItem.gameObject);
        equippedWeaponID = id;
        equippedItem.RpcOwnershipSetup(Owner.id);
        RpcEquipItem(equippedItem.gameObject);
    }

    [ClientRpc]
    private void RpcSetWeaponParent(GameObject weapon) {
        weapon.transform.SetParent(weaponPivot);
        weapon.transform.rotation = weaponPivot.transform.rotation;
        weapon.transform.localPosition = Vector3.zero;
    }
    
    [ClientRpc]
    public void RpcEquipItem(GameObject gObject) {
        equippedItem = gObject.GetComponent<Item>();
        equippedItem.Init(input);

        if(hasAuthority)
            enabled = true;
    }

    public void ParentWeapon(Transform weapon) {
        weapon.transform.SetParent(weaponPivot);
    }

    public void UnEquipWeapon() {
        Destroy(equippedItem.gameObject);
        equippedItem = null;
    }

    public void AddWeapon(Item weapon) {
        equippedItem = weapon;
        equippedItem.Init(input);
    }

    private void Update() {
        if (equippedItem == null || !hasAuthority)
            return;

        equippedItem.Tick();
    }
}
