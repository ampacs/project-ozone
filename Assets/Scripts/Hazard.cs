using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hazard : Damageable {

    public enum Type {
        Suicide, Laser, Shield
    }

    public enum FacingMode {
        Movement, Planet, Player
    }

    public Type type;
    public int shield;
    public int maxShield;
    public float acceleration;
    public float maxSpeed;
    public Path path;
    public FacingMode mode;

    protected bool _faceMovementDirection;
    protected bool _facePlanet;
    protected bool _facePlayer;
    protected int _currentPoint;
    protected float _speed;
    protected float _maxSpeedSqr;
    protected Rigidbody _rigidbody;

    public override void Damage(int damage) {
        if (shield > 0) {
            shield -= damage;
            if (shield < 0)
                health += shield;
        } else {
            health -= damage;
        }
        if (health <= 0) {
            Kill();
        }
    }

    protected virtual void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _maxSpeedSqr = maxSpeed * maxSpeed;
    }

    protected virtual void Update() {
        switch (mode) {
            case FacingMode.Movement:
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
                break;
            case FacingMode.Planet:
                transform.rotation = Quaternion.LookRotation(PlanetController.current.transform.position - transform.position);
                break;
            case FacingMode.Player:
                transform.rotation = Quaternion.LookRotation(PlayerMovementController.current.transform.position - transform.position);
                break;
        }
    }

    protected virtual void FixedUpdate() {
        if ((transform.position - path.waypoints[_currentPoint].position).sqrMagnitude < 0.25f) {
            _currentPoint = path.GetNextIndex(_currentPoint);
        }
        _rigidbody.AddForce(path.waypoints[_currentPoint].position - transform.position, ForceMode.Acceleration);
        if (_rigidbody.velocity.sqrMagnitude > _maxSpeedSqr) {
            _rigidbody.velocity = _rigidbody.velocity.normalized * maxSpeed;
        }
    }
}
