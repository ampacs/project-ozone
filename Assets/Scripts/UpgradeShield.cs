using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShield : Upgrade {
    public GameObject collectionCollider;
    public GameObject[] destructionCollider;
    public override void Use() {
        if (used) return;
        PlayerShieldController.current.DeactivateShield();
        destructionCollider[level - 1].SetActive(true);
        Destroy(gameObject, useTime);
        base.Use();
    }

    // Update is called once per frame
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
            } else if (GameManager.current.currentPowerup.GetComponent<Upgrade>().type == Upgrade.Type.Shield) {
                Upgrade currentPowerup = GameManager.current.currentPowerup.GetComponent<Upgrade>();
                if (currentPowerup.level < currentPowerup.maxLevel) {
                    currentPowerup.level++;
                    Destroy(gameObject);
                }
            }
        } else if (other.gameObject.tag == "Hazard") {
            other.GetComponent<Damageable>().Damage(damage);
        }
    }
    
    void OnValidate() {
        type = Type.Shield;
    }
}
