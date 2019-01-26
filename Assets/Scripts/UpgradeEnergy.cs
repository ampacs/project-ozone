using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEnergy : Upgrade {

    public float energy;

    // Start is called before the first frame update
    void Start() {
        GameManager.current.AddEnergy(energy);
    }

    void FixedUpdate() {
        if (Time.fixedTime > spawnMoment + lifetime) {
            Destroy(gameObject);
        }
    }
}
