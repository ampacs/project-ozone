using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLaser : Upgrade {

    public GameObject collectionCollider;
    public GameObject destructionCollider;

    public override void Use() {
        if (used) return;
        destructionCollider.SetActive(true);
        Destroy(gameObject, useTime * level);
        base.Use();
    }

    protected override void Update() {
        base.Update();
        if (Time.time > spawnMoment + lifetime) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (!collected) {
            if (GameManager.current.currentPowerup == null) {
                collected = true;
                GameManager.current.AddPowerup(gameObject);
                collectionCollider.SetActive(false);
            } else if (GameManager.current.currentPowerup.GetComponent<Upgrade>().type == Upgrade.Type.Laser) {
                Upgrade currentPowerup = GameManager.current.currentPowerup.GetComponent<Upgrade>();
                if (currentPowerup.level < currentPowerup.maxLevel) {
                    currentPowerup.level++;
                    Destroy(gameObject);
                }
            }
        } else if (other.gameObject.tag == "Hazard") {
            other.gameObject.transform.parent.GetComponent<Hazard>().Damage(damage);
        }
    }
}
