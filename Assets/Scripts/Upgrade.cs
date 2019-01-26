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
    public float lifetime;
    protected float spawnMoment;

    void Awake() {
        spawnMoment = Time.fixedTime;
    }
}
