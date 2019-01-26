using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager current;

    public float energy;

    public int numberFocusedFacilities = -1;
    public int numberNonSupportFacilities = -1;

    public Transform[] facilitiesSpawnPoints;
    public GameObject[] facilities;
    public GameObject planet;

    bool[] _focusedFacility;
    Facility[] _facilities;

    public void AddEnergy(float energy) {
        this.energy += energy;
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);//, SceneManagement.LoadSceneMode.Single);
    }

    /* /void LoadScene(string sceneName,  SceneManagement.LoadSceneMode mode) {
        SceneManager.LoadScene(sceneName, mode);
    }/* */

    void Awake() {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);    
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name == "Game") {
            if (facilitiesSpawnPoints.Length == 0) {
                GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("FacilitySpawnPoint");
                facilitiesSpawnPoints = new Transform[spawnPoints.Length];
                for (int i = 0; i < spawnPoints.Length; i++) {
                    facilitiesSpawnPoints[i] = spawnPoints[i].transform;
                }
            }
            if (numberNonSupportFacilities < 0) {
                _focusedFacility = new bool[facilities.Length];
                _facilities = new Facility[facilities.Length];
                numberNonSupportFacilities = 0;
                for (int i = 0; i < facilities.Length; i++) {
                    _facilities[i] = facilities[i].GetComponent<Facility>();
                    if (_facilities[i].type != Facility.Type.Support) {
                        numberNonSupportFacilities++;
                    }
                    _focusedFacility[i] = true;
                }
                numberFocusedFacilities = numberNonSupportFacilities;
            }

            for (int i = 0; i < facilities.Length; i++) {
                if (_focusedFacility[i])
                    _facilities[i].AddEnergy(energy/numberFocusedFacilities);
            }
        }
    }
}
