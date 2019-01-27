using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fetcher : MonoBehaviour
{

    public GameObject facility;
    Button thisButton;
 
    void Start()
    {
        FacilityTypeManager man = GameObject.FindWithTag("FacilityTypeManager").GetComponent<FacilityTypeManager>();
        thisButton = GetComponent<Button>();
        //thisButton.onClick.AddListener(man.AddFacility);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
