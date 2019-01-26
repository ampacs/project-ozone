using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldController : MonoBehaviour {

    public static PlayerShieldController current;
    public GameObject shield;

    public float offTime;

    float offMoment;
    public void DeactivateShield() {
        offMoment = Time.time;
        shield.SetActive(false);
    }

    void Awake() {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);
    }

    void Update() {
        if (!shield.activeInHierarchy && Time.time > offMoment + offTime) {
            shield.SetActive(true);
        }
    }
}
