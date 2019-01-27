using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : BaseObject {

    public int damage;
    public int health;
    public int maxHealth;
    public float destructionTime;

    public virtual void Damage (int damage) {
        health -= damage;
        if (health <= 0) {
            Kill();
        }
    }

    public virtual void Kill () {
        Destroy(gameObject, destructionTime);
    }

    protected virtual void OnCollisionEnter(Collision collisionInfo) {
        Damageable damageable = collisionInfo.collider.gameObject.transform.parent.GetComponent<Damageable>();
        if (damageable != null)
            damageable.Damage(damage);
    }

    protected virtual void OnTriggerEnter(Collider other) {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable != null)
            damageable.Damage(damage);
    }
}
