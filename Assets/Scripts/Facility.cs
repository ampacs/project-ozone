using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facility : Damageable {

    public enum Type {
        Offensive, Defensive, SupportClean, SupportFossil
    }
    public Type type;
    public bool activateUpgradeOnSpawn;
    public int damageToHome;
    public float energy;
    public float maxEnergy;
    public bool regenerating;
    public float regenerationTime;

    public float upgradeSpawnPeriod;
    public float upgradeTargetDistance = 1.5f;

    public GameObject upgrade;

    public float lastUpgradeSpawnMoment;
    float _destructionMoment = -100f;

    public override void Kill() {
        _destructionMoment = Time.time;
        regenerating = true;
    }

    public void AddEnergy (float energy) {
        if (regenerating)
            return;
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

    void ManageSupportFossilFacility () {
        if (Time.fixedTime > lastUpgradeSpawnMoment + upgradeSpawnPeriod) {
            lastUpgradeSpawnMoment = Time.fixedTime;
            SpawnUpgrade(transform.position, transform.rotation, null, transform.forward * upgradeTargetDistance);
            PlanetController.current.Damage(damageToHome);
        }
    }

    void ManageSupportCleanFacility () {
        if (Time.fixedTime > lastUpgradeSpawnMoment + upgradeSpawnPeriod) {
            lastUpgradeSpawnMoment = Time.fixedTime;
            SpawnUpgrade(transform.position, transform.rotation, null, transform.forward * upgradeTargetDistance);
        }
    }

    void FixedUpdate() {
        if (Time.time < _destructionMoment + regenerationTime) {
            health = Mathf.RoundToInt((Time.time - _destructionMoment)/regenerationTime * maxHealth);
            return;
        }
        regenerating = false;
        switch (type) {
            case Type.Defensive: break;
            case Type.Offensive:
                //ManageOffensiveFacility();
                break;
            case Type.SupportClean:
                ManageSupportCleanFacility();
                break;
            case Type.SupportFossil:
                ManageSupportFossilFacility();
                break;
        }
    }
}
