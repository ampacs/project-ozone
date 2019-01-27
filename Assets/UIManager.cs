using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    
    [System.Serializable]
    public struct FacilityUI {
        public enum State {
            active, inactive, onHold
        }
        [HideInInspector]
        public string key;
        public GameObject activeState;
        public GameObject inactiveState;
        public GameObject onHoldState;
        public float resourceWidth;
        public RectTransform resource;
        public GameObject attackIcon;
        public GameObject defenseIcon;
        public GameObject supportIcon;

        public void SetState(State state) {
            activeState.SetActive(false);
            inactiveState.SetActive(false);
            onHoldState.SetActive(false);
            switch (state) {
                case State.active:
                    activeState.SetActive(true);
                    break;
                case State.inactive:
                    inactiveState.SetActive(true);
                    break;
                case State.onHold:
                    onHoldState.SetActive(true);
                    break;
            }
        }

        public void SetIcon(Facility.Type type) {
            attackIcon.SetActive(false);
            defenseIcon.SetActive(false);
            supportIcon.SetActive(false);
            switch (type) {
                case Facility.Type.Offensive:
                    attackIcon.SetActive(true);
                    break;
                case Facility.Type.Defensive:
                    defenseIcon.SetActive(true);
                    break;
                case Facility.Type.SupportClean:
                case Facility.Type.SupportFossil:
                    supportIcon.SetActive(true);
                    break;
            }
        }

        public void SetResourceAmount (float percentage) {
            SetResourceAmount(percentage, 10f);
        }

        public void SetResourceAmount (float percentage, float updateRate) {
            if (percentage * resourceWidth < resource.sizeDelta.x)
                resource.sizeDelta = new Vector2(0f, resource.sizeDelta.y);
            else {
                float width = Mathf.Lerp(resource.sizeDelta.x, percentage * resourceWidth, updateRate*Time.deltaTime);
                resource.sizeDelta = new Vector2(width, resource.sizeDelta.y);
            }
        }
    }
    public int currentYear;
    public int startYear = 2100;
    public int yearChangeRate = 10;

    public Text year;
    public GameObject upgradeClassAttack;
    public GameObject upgradeClassDefense;
    public Text upgradeLevel;
    public FacilityUI[] facilities;

    float _lastMomentYearChanged;

    void SetCurrentUpgrade (Upgrade.Type type) {
        upgradeClassAttack.SetActive(false);
        upgradeClassDefense.SetActive(false);
        switch (type) {
            case Upgrade.Type.Laser:
                upgradeClassAttack.SetActive(true);
                break;
            case Upgrade.Type.Shield:
                upgradeClassDefense.SetActive(true);
                break;
            default:
                break;
        }
    }

    void Start() {
        currentYear = startYear;
        _lastMomentYearChanged = Time.time;
    }

    void Update() {
        if (Time.time > _lastMomentYearChanged + yearChangeRate) {
            _lastMomentYearChanged = Time.time;
            currentYear += 1;
            year.text = currentYear.ToString();
        }

        for (int i = 0; i < facilities.Length; i++) {
            Facility facility = GameManager.current.facilities[i].GetComponent<Facility>();
            if (facility.regenerating) {
                facilities[i].SetState(FacilityUI.State.inactive);
                // facilities[i].SetResourceAmount(facility.health/facility.maxHealth);
            } else if (!GameManager.current.focusedFacility[i] && facility.type != Facility.Type.SupportClean && facility.type != Facility.Type.SupportFossil) {
                facilities[i].SetState(FacilityUI.State.onHold);
            } else if (facility.type == Facility.Type.SupportClean || facility.type == Facility.Type.SupportFossil) {
                facilities[i].SetState(FacilityUI.State.active);
                facilities[i].SetResourceAmount((Time.time - facility.lastUpgradeSpawnMoment)/facility.upgradeSpawnPeriod, 50f);
            } else {
                facilities[i].SetState(FacilityUI.State.active);
                facilities[i].SetResourceAmount(facility.energy/facility.maxEnergy, 6f);
            }
            facilities[i].SetIcon(facility.type);
        }

        if (GameManager.current.currentPowerup != null) {
            SetCurrentUpgrade(GameManager.current.currentPowerup.GetComponent<Upgrade>().type);
            upgradeLevel.text = GameManager.current.currentPowerup.GetComponent<Upgrade>().level.ToString();
        } else {
            SetCurrentUpgrade(Upgrade.Type.Energy);
            upgradeLevel.text = "0";
        }
    }
}
