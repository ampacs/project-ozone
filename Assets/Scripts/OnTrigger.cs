﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        Debug.Log("Hello!");
    }

    void OnTriggerStay(Collider other) {
        Debug.Log("Hello?");
    }

    void OnTriggerExit(Collider other) {
        Debug.Log("Heyo?");
    }
}
