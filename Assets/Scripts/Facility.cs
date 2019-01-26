using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour {

    public enum Type {
        Offensive, Defensive, Support
    }
    public new string name;
    public string description;
    public Type type;

    public bool activateUpgradeOnSpawn;

    public float health;
    public float maxHealth;
    public float energy;
    public float maxEnergy;
    public float regenerationTime;

    public float upgradeSpawnPeriod;

    public GameObject upgrade;

    float lastUpgradeSpawnMoment;

    public void AddEnergy (float energy) {
        this.energy = this.energy + energy > maxEnergy ? maxEnergy : this.energy + energy;
    }

    public void SpawnUpgrade () {
        
    }

    void ManageSupportFacility () {
        if (Time.fixedTime > lastUpgradeSpawnMoment + upgradeSpawnPeriod) {
            lastUpgradeSpawnMoment = Time.fixedTime;
            
        }
    }

    void FixedUpdate() {
        switch (type) {
            case Type.Defensive: break;
            case Type.Offensive: break;
            case Type.Support:
                ManageSupportFacility();
                break;
        }
    }
}
