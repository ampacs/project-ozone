using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityTypeManager : MonoBehaviour {

    public static FacilityTypeManager current;
    public GameObject[] facilities;
    public int[] facilityAmount;

    void Awake() {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);    
        DontDestroyOnLoad(gameObject);
    }
}
