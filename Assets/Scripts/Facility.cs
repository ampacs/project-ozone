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
    public float upgradeTargetDistance = 1.5f;

    public GameObject upgrade;

    float lastUpgradeSpawnMoment;

    public void AddEnergy (float energy) {
        this.energy += energy;
        if (this.energy > maxEnergy) {
            this.energy -= maxEnergy;
            SpawnUpgrade(transform.position, transform.rotation, null, transform.forward * upgradeTargetDistance);
        }
    }

    public GameObject SpawnUpgrade (Vector3 position, Quaternion rotation, Transform parent, Vector3 target) {
        GameObject obj = Instantiate(upgrade, position, rotation, parent);
        obj.GetComponent<Upgrade>().targetPosition = target;
        return obj;
    }

    void ManageOffensiveFacility () {

    }

    void ManageSupportFacility () {
        if (Time.fixedTime > lastUpgradeSpawnMoment + upgradeSpawnPeriod) {
            lastUpgradeSpawnMoment = Time.fixedTime;
            GameObject instantiated = SpawnUpgrade(transform.position, transform.rotation, null, transform.forward * upgradeTargetDistance);
        }
    }

    void FixedUpdate() {
        switch (type) {
            case Type.Defensive: break;
            case Type.Offensive:
                //ManageOffensiveFacility();
                break;
            case Type.Support:
                ManageSupportFacility();
                break;
        }
    }
}
