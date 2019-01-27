using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardLaser : Hazard {

    public int ammo = 5;
    public float fireRate = 2;

    int _currentAmmo = 0;
    float _lastShotMoment;

    public void Shoot () {

    }

    protected override void Update () {
        if (ammo == 0) {
            points = HazardWaypointManager.current.GetExitPath(transform.position).waypoints;
            mode = FacingMode.Movement;
            ammo--;
        } else if (ammo > 0) {
            if (Time.time > _lastShotMoment + fireRate) {
                Shoot();
            }
        }
        base.Update();
    }

    void OnValidate() {
        type = Type.Laser;
    }
}
