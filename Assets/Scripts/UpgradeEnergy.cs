using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEnergy : Upgrade {

    public float energy;

    // Start is called before the first frame update
    protected override void Start() {
        GameManager.current.AddEnergy(energy);
        Destroy(gameObject, lifetime);
    }
}
