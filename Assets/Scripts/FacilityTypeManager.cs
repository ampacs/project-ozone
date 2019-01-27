using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityTypeManager : MonoBehaviour {

    public static FacilityTypeManager current;
    public GameObject[] facilities;
    public int[] facilityAmount;

    public void AddFacility(GameObject facility) {
        AddFacility(facility.GetComponent<Facility>().type);
    }

    public void AddFacility(Facility.Type type) {
        for (int i = 0; i < facilities.Length; i++) {
            if (facilities[i].GetComponent<Facility>().type == type) {
                facilityAmount[i]++;
                GameManager.current.AddFacility(facilities[i]);
                return;
            }
        }
    }

    public void RemoveFacility(Facility.Type type) {
        for (int i = 0; i < facilities.Length; i++) {
            if (facilities[i].GetComponent<Facility>().type == type) {
                facilityAmount[i]--;
                return;
            }
        }
    }

    void Awake() {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);    
        DontDestroyOnLoad(gameObject);
    }
}
