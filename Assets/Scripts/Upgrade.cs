using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : BaseObject {

    public enum Type {
        Energy, Shield, Laser
    }
    public Type type;
    public int level = 1;
    public int maxLevel = 1;
    public int damage;
    public float useTime;
    public float lifetime;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public float speed = 0.5f;
    public AnimationCurve distanceOverDistance;
    public float baseMeshSize = 0.5f;
    protected bool collected;
    protected bool used;
    protected float spawnMoment;
    protected float distance;
    protected MeshRenderer mesh;
    protected Vector3 referenceScale;

    public void ResetLifetime() {
        spawnMoment = Time.time;
    }

    public virtual void Use() {
        used = true;
    }

    void Awake() {
        spawnMoment = Time.time;
        startPosition = transform.position;
        distance = Vector3.Distance(startPosition, targetPosition);
    }

    protected virtual void Start() {
        mesh = GetComponentInChildren<MeshRenderer>();
        referenceScale = mesh.transform.localScale;
    }

    protected virtual void Update() {
        float distanceCovered = (Time.time - spawnMoment) * speed;
        float fracJourney = distanceCovered / distance;
        transform.position = Vector3.Lerp(startPosition, targetPosition, distanceOverDistance.Evaluate(fracJourney));
        if (mesh != null && collected)
            mesh.transform.localScale = Vector3.Lerp(mesh.transform.localScale, baseMeshSize * level * referenceScale, 5*Time.deltaTime);
    }
}
