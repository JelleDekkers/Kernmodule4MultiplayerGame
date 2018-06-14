using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticFireWeapon : Item {

    public AutomaticFireWeaponData data;
    public override ItemData Data {
        get { return data; }
    }

    protected CharacterControllerInput input;

    private bool canFire;

    public override void Init(CharacterControllerInput input) {
        this.input = input;
    }

    public override void Tick() {
        if (input.FireKeyPressed && canFire)
            Fire();
    }

    protected virtual void Fire() {
        Projectile projectile = Instantiate(data.projectilePrefab, owner.character.transform.position + owner.character.transform.forward, owner.character.transform.rotation, null);
        projectile.RpcInit(owner.id, id);
        canFire = false;
        owner.StartCoroutine(WaitForNextShot());
    }

    private IEnumerator WaitForNextShot() {
        float coolDownTimer = data.coolDownBetweenShots;
        while (coolDownTimer > 0) {
            coolDownTimer -= Time.deltaTime;
            yield return null;
        }
        canFire = true;
    }
}
