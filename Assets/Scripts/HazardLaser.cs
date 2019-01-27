using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardLaser : Hazard {

    public int ammo = 5;
    public float fireRate = 2;
    public float maximumDistance = 3f;

    int _currentAmmo = 0;
    float _lastShotMoment;
    float _maximumDistanceSqr;

    public void Shoot () {
        LaserBeam beam = GetComponentInChildren<LaserBeam>();
        beam.Shoot(damage);
    }

    protected override void Start() {
        _currentAmmo = ammo;
        _maximumDistanceSqr = maximumDistance * maximumDistance;
        base.Start();
    }

    protected override void Update () {
        if (_currentAmmo == 0) {
            path = HazardWaypointManager.current.GetExitPath(transform.position);
            mode = FacingMode.Movement;
            _currentAmmo--;
        } else if (_currentAmmo > 0) {
            if (Time.time > _lastShotMoment + fireRate && (PlanetController.current.transform.position - transform.position).sqrMagnitude < _maximumDistanceSqr) {
                _lastShotMoment = Time.time;
                _currentAmmo--;
                Shoot();
            }
        }
        base.Update();
    }

    void OnValidate() {
        type = Type.Laser;
    }
}
