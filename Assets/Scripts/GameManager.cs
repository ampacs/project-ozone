using System;
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
    public GameObject currentPowerup;


    public AudioSource titleTheme;
    public AudioSource gameTheme;
    public bool[] focusedFacility;
    int _year;
    Facility[] _facilities;

    public void AddEnergy(float energy) {
        this.energy += energy;
    }

    public void AddPowerup(GameObject powerup) {
        currentPowerup = powerup;
        GameObject powerupTarget = GameObject.FindWithTag("PowerupTarget");
        currentPowerup.transform.parent = powerupTarget.transform;
        currentPowerup.transform.position = powerupTarget.transform.position;
    }

    public void AddFacility(GameObject facility) {
        for (int i = 0; i < facilities.Length; i++) {
            if (facilities[i] == null) {
                facilities[i] = facility;
                return;
            }
        }
    }

    public void RemoveLastFacility() {
        for (int i = facilities.Length - 1; i >= 0; i--) {
            if (facilities[i] != null) {
                FacilityTypeManager.current.RemoveFacility(facilities[i].GetComponent<Facility>().type);
                facilities[i] = null;
                return;
            }
        }
    }

    public int GetNumberOfAddedFacilities() {
        int n = 0;
        for (int i = 0; i < facilities.Length; i++) {
            if (facilities[i] != null) {
                n++;
            }
        }
        return n;
    }

    public int GetNumberOfSupportFacilities() {
        int n = 0;
        for (int i = 0; i < facilities.Length; i++) {
            if (facilities[i] == null)
                continue;
            switch (facilities[i].GetComponent<Facility>().type) {
                case Facility.Type.SupportClean:
                case Facility.Type.SupportFossil:
                    n++;
                    break;
                default: break;
            }
        }
        return n;
    }

    public void RemovePowerup() {
        Destroy(currentPowerup);
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

                int f = 0;
                for (int i = 0; i < FacilityTypeManager.current.facilities.Length; i++) {
                    for (int j = 0; f < facilities.Length && j < FacilityTypeManager.current.facilityAmount[i]; f++, j++) {
                        facilities[f] = Instantiate(FacilityTypeManager.current.facilities[i],
                                                    facilitiesSpawnPoints[f].position,
                                                    Quaternion.LookRotation(facilitiesSpawnPoints[f].transform.forward),
                                                    facilitiesSpawnPoints[f]);
                    }
                }
            }
            if (numberNonSupportFacilities < 0) {
                focusedFacility = new bool[facilities.Length];
                _facilities = new Facility[facilities.Length];
                numberNonSupportFacilities = 0;
                for (int i = 0; i < facilities.Length; i++) {
                    _facilities[i] = facilities[i].GetComponent<Facility>();
                    if (_facilities[i].type != Facility.Type.SupportClean && _facilities[i].type != Facility.Type.SupportFossil) {
                        numberNonSupportFacilities++;
                        focusedFacility[i] = true;
                    }
                }
                numberFocusedFacilities = numberNonSupportFacilities;
            }

            for (int i = 0; i < facilities.Length; i++) {
                if (focusedFacility[i])
                    _facilities[i].AddEnergy(energy/(float)numberFocusedFacilities);
            }
            energy = 0f;

            if (Input.GetButtonDown("Jump") && currentPowerup != null) {
                currentPowerup.GetComponent<Upgrade>().Use();
            }

            if (Input.GetButtonDown("Fire1") && !facilities[0].GetComponent<Facility>().regenerating && facilities[0].GetComponent<Facility>().type != Facility.Type.SupportClean && facilities[0].GetComponent<Facility>().type != Facility.Type.SupportFossil)
                focusedFacility[0] = !focusedFacility[0];
            if (Input.GetButtonDown("Fire2") && !facilities[1].GetComponent<Facility>().regenerating && facilities[1].GetComponent<Facility>().type != Facility.Type.SupportClean && facilities[1].GetComponent<Facility>().type != Facility.Type.SupportFossil)
                focusedFacility[1] = !focusedFacility[1];
            if (Input.GetButtonDown("Fire3") && !facilities[2].GetComponent<Facility>().regenerating && facilities[2].GetComponent<Facility>().type != Facility.Type.SupportClean && facilities[2].GetComponent<Facility>().type != Facility.Type.SupportFossil)
                focusedFacility[2] = !focusedFacility[2];
            
            numberFocusedFacilities = 0;
            for (int i = 0; i < focusedFacility.Length; i++) {
                if (focusedFacility[i])
                    numberFocusedFacilities++;
            }
            if (titleTheme.isPlaying) {
                titleTheme.Stop();
            }
            if (!gameTheme.isPlaying) {
                gameTheme.Play();
            }
        } else if (SceneManager.GetActiveScene().name == "Main Menu") {
            if (Input.GetButtonDown("Cancel")) {
                RemoveLastFacility();
            }
            if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit")) && GetNumberOfAddedFacilities() == 4) {
                GameManager.current.LoadScene("Game");
            }
            if (!titleTheme.isPlaying) {
                titleTheme.Play();
            }
            if (gameTheme.isPlaying) {
                gameTheme.Stop();
            }
        } else if (SceneManager.GetActiveScene().name == "Title Menu") {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit")) {
                GameManager.current.LoadScene("Main Menu");
            }
            if (!titleTheme.isPlaying) {
                titleTheme.Play();
            }
            if (gameTheme.isPlaying) {
                gameTheme.Stop();
            }
        }
    }
}
