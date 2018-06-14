using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SingleFireWeapon : Item {

    public WeaponData data;
    public override ItemData Data {
        get { return data; }
    }

    protected CharacterControllerInput input;

    [SerializeField]
    private Muzzle muzzle;

    public override void Init(CharacterControllerInput input) {
        this.input = input; 
    }

    public override void Tick() {
        if (input == null)
            return;

        if (input.FireKeyPressed)
            CmdFire();
    }

    [Command]
    protected virtual void CmdFire() {
        Projectile projectile = Instantiate(data.projectilePrefab, muzzle.transform.position, muzzle.transform.rotation, null) as Projectile;
        NetworkServer.Spawn(projectile.gameObject);
        projectile.RpcInit(owner.id, id);
        projectile.transform.SetParent(owner.transform);
        RpcSetProjectileParent(projectile.gameObject);
    }

    [ClientRpc]
    void RpcSetProjectileParent(GameObject projectile) {
        projectile.transform.SetParent(owner.transform);
    }
}

