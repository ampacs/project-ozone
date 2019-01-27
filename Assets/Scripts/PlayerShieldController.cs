using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldController : MonoBehaviour {

    public static PlayerShieldController current;
    public GameObject shield;

    public int damage = 100;
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

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Hazard") {
            other.gameObject.transform.parent.GetComponent<Hazard>().Damage(damage);
        }
    }
}
