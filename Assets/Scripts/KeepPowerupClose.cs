using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPowerupClose : MonoBehaviour {

    void LateUpdate() {
        if (transform.childCount > 0) {
            Transform powerup = transform.GetChild(0);
            powerup.gameObject.GetComponent<Upgrade>().ResetLifetime();
            powerup.position = transform.position;
            powerup.rotation = Quaternion.LookRotation(transform.forward);
        }
    }
}
