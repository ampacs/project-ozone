using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour {

    public enum Type {
        Energy, Other
    }

    public new string name;
    public string description;
    public Type type;
    public float useTime;
    public float lifetime;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public float speed = 0.5f;
    public AnimationCurve distanceOverDistance;
    protected bool collected;
    protected bool used;
    protected float spawnMoment;
    protected float distance;

    public void ResetLifetime() {
        spawnMoment = Time.time;
    }

    public virtual void Use() {}

    void Awake() {
        spawnMoment = Time.time;
        startPosition = transform.position;
        distance = Vector3.Distance(startPosition, targetPosition);
    }

    protected virtual void Update() {
        float distanceCovered = (Time.time - spawnMoment) * speed;
        float fracJourney = distanceCovered / distance;
        transform.position = Vector3.Lerp(startPosition, targetPosition, distanceOverDistance.Evaluate(fracJourney));
    }
}
