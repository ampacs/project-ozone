using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLaser : Upgrade {

    public GameObject collectionCollider;
    public GameObject destructionCollider;

    public override void Use() {
        if (used) return;
        destructionCollider.SetActive(true);
        Destroy(gameObject, useTime);
        base.Use();
    }

    protected override void Update() {
        base.Update();
        if (Time.time > spawnMoment + lifetime) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (!collected && GameManager.current.currentPowerup == null) {
            collected = true;
            GameManager.current.AddPowerup(gameObject);
            collectionCollider.SetActive(false);
        } else if (other.gameObject.tag == "Hazard") {
            other.GetComponent<Damageable>().Damage(damage);
        }
    }
}
