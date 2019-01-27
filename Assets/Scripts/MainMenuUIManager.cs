using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour {

    [System.Serializable]
    public struct SelectionBox {
        public GameObject selectionBox;
        public GameObject attackImage;
        public GameObject defenseImage;
        public GameObject supportImage;

        public void SetImage(Facility.Type type) {
            RemoveImage();
            switch (type) {
                case Facility.Type.Defensive:
                    defenseImage.SetActive(true);
                    break;
                case Facility.Type.Offensive:
                    attackImage.SetActive(true);
                    break;
                case Facility.Type.SupportClean:
                case Facility.Type.SupportFossil:
                    supportImage.SetActive(true);
                    break;
            }
        }

        public void RemoveImage() {
            attackImage.SetActive(false);
            defenseImage.SetActive(false);
            supportImage.SetActive(false);
        }
    }

    public GameObject[] classButtons;

    public GameObject[] tooltips;

    public SelectionBox[] selectionBox;
    public bool[] selected;

    public void ShowTooltip(int index) {
        for (int i = 0; i < tooltips.Length; i++) {
            tooltips[i].SetActive(false);
        }
        tooltips[index].SetActive(true);
    }

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.sqrMagnitude > 0f && EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(classButtons[0]);
        }
        for (int i = 0; i < classButtons.Length; i++) {
            if (GameObject.ReferenceEquals(EventSystem.current.currentSelectedGameObject, classButtons[i])) {
                ShowTooltip(i);
            }
        }
        for (int i = 0; i < GameManager.current.facilities.Length; i++) {
            if (GameManager.current.facilities[i] != null)
                selectionBox[i].SetImage(GameManager.current.facilities[i].GetComponent<Facility>().type);
            else selectionBox[i].RemoveImage();
        }
        bool needToSelectSupport = !(GameManager.current.GetNumberOfAddedFacilities() == 3 && GameManager.current.GetNumberOfSupportFacilities() == 0);
        for (int i = 2; i < 4; i++) {
            classButtons[i].GetComponent<Button>().interactable = needToSelectSupport;
            if (!needToSelectSupport) {
                classButtons[i].GetComponent<Animator>().SetTrigger("Disabled");
            }
        }
    }
}
