using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuUIManager : MonoBehaviour {

    public GameObject firstSelectedGameObject;

    public GameObject[] classButtons;

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.sqrMagnitude > 0f && EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(firstSelectedGameObject);
        }
        
    }
}
