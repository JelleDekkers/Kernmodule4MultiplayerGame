using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : Projectile {

    public float speed;
    public new Rigidbody rigidbody;
    public new Collider collider;
    public BulletTrail trail;

    private void Start() {
        Freeze();
    }

    public override void InitExtraFunc(int playerID, int itemID) {
        base.InitExtraFunc(playerID, itemID);
        trail.Init(player.color);
    }

    private void OnCollisionEnter(Collision collision) {
        IDamageable damagableObject = collision.gameObject.GetInterface<IDamageable>();
        if (damagableObject != null) {
            OnHit(damagableObject);
            Destroy(gameObject);
            return;
        }

        if(!collision.gameObject.GetComponent<Bullet>())
            Destroy(gameObject);
    }

    private void OnHit(IDamageable hittableObject) {
        SingleFireWeaponData d = WeaponFiredFrom.Data as SingleFireWeaponData;
        hittableObject.Hit(new DamageInfo(player.id, WeaponFiredFrom.id, d.damagePerProjectile));
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        rigidbody.velocity = transform.forward * speed;
    }

    private void Freeze() {
        rigidbody.isKinematic = true;
        collider.enabled = false;
    }

    private void UnFreeze() {
        rigidbody.isKinematic = false;
        collider.enabled = true;
    }

    public override void OnNewTurn(int turnNr, int playerID) {
        if (player.id == playerID)
            StartCoroutine(Move());
    }

    public override void OnTurnEnded() {
        Freeze();
    }

    private IEnumerator Move() {
        UnFreeze();
        float timer = unFreezeTimeOnTurn.runTimeValue;
        while(timer > 0) {
            timer -= Time.deltaTime;
            yield return null;
        }
        Freeze();
    }
}
