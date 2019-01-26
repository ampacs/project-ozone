using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShieldPermanent : BaseObject
{
    public int damage;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Hazard") {
            other.GetComponent<Damageable>().Damage(damage);
        }
    }
}
