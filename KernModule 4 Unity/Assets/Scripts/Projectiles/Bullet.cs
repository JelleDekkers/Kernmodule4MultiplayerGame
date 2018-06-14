using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : Projectile {

    public float speed;
    public new Rigidbody rigidbody;
    public new Collider collider;

    private void Start() {
        Freeze(); 
    }

    private void OnCollisionEnter(Collision collision) {
        IDamageable damagableObject = collision.gameObject.GetInterface<IDamageable>();
        if (damagableObject != null) 
            OnHit(damagableObject);
        if(!collider.GetComponent<Bullet>())
            Destroy(gameObject);
    }

    private void OnHit(IDamageable hittableObject) {
        hittableObject.Hit(new DamageInfo(player.id, player.id, WeaponFiredFrom.id));
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
