using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public static EnemyController current;

    public enum WaveMode {
        Random, CustomRandom, CustomSpecific
    }

    [System.Serializable]
    public struct WaveTypeDistribution {
        [HideInInspector]
        public string key;
        public Hazard.Type enemyType;
        public AnimationCurve enemyChance;
    }
    [System.Serializable]
    public struct Wave {
        [HideInInspector]
        public string key;
        public WaveMode mode;
        public AnimationCurve waveTimeLength;
        public AnimationCurve hazardsPerWave;
        public WaveTypeDistribution[] enemyTypeDistribution;
    }
    [System.Serializable]
    public struct Enemy {
        [HideInInspector]
        public string key;
        public int index;
        public Hazard.Type enemyType;
        public Vector3 spawnPosition;
    }
    public Hazard[] spawnableHazards;
    public AnimationCurve spawnDistanceFromPlanet;
    public AnimationCurve timeBetweenWaves;
    public AnimationCurve timePerWave;
    public AnimationCurve hazardsPerWave;
    public WaveTypeDistribution[] enemyTypeDistribution = new WaveTypeDistribution[0];
    public Wave[] initialWaves = new Wave[0];

    int _currentWave = -1;
    int _currenHazard = 0;
    int _numberOfHazards = 0;
    float _waveTimeLength = 0f;
    float _waveTimeBreather = 0f;
    float _waveStartMoment;
    float _lastHazardSpawnedMoment;
    WaveTypeDistribution[] _waveHazardDistribution;
    Hazard.Type[] _currentTypeOrder;

    public Enemy[] _waveHazards;

    public Vector3 GetRandomLocationOnCircle(float distanceFromCenter) {
        Vector2 location = Random.insideUnitCircle.normalized * distanceFromCenter;
        return new Vector3(location.x, 0f, location.y);
    }

    public int GetRandomIndex(float[] probabilities) {
        float probabilitiesSum = probabilities[0];
        float[] adaptedProbabilities = new float[probabilities.Length];

        adaptedProbabilities[0] = probabilities[0];
        for (int i = 1; i < probabilities.Length; i++) {
            adaptedProbabilities[i] = adaptedProbabilities[i - 1] + probabilities[i];
            probabilitiesSum += probabilities[i];
        }

        float value = Random.Range(0f, probabilitiesSum);
        for (int i = 0; i < probabilities.Length - 1; i++) {
            if (value <= adaptedProbabilities[i])
                return i;
        }
        return probabilities.Length - 1;
    }

    public void GenerateWaveEnemies () {
        _currentWave++;
        _currenHazard = 0;
        if (_currentWave < initialWaves.Length) {
            _numberOfHazards = Mathf.RoundToInt(initialWaves[_currentWave].hazardsPerWave.Evaluate(Random.value));
            _waveTimeLength = initialWaves[_currentWave].waveTimeLength.Evaluate(Random.value);
            _waveHazardDistribution = initialWaves[_currentWave].enemyTypeDistribution;
        } else {
            _numberOfHazards = Mathf.RoundToInt(hazardsPerWave.Evaluate(Random.value));
            _waveTimeLength = timePerWave.Evaluate(Random.value);
            _waveHazardDistribution = enemyTypeDistribution;
        }
        _waveTimeBreather = timeBetweenWaves.Evaluate(Random.value);
        _waveHazards = new Enemy[_numberOfHazards];
        _currentTypeOrder = new Hazard.Type[_waveHazardDistribution.Length];
        for (int i = 0; i < _waveHazardDistribution.Length; i++) {
            _currentTypeOrder[i] = _waveHazardDistribution[i].enemyType;
        }

        for (int i = 0; i < _numberOfHazards; i++) {
            float[] probabilities = new float[_waveHazardDistribution.Length];
            for (int j = 0; j < _waveHazardDistribution.Length; j++) {
                probabilities[j] = _waveHazardDistribution[j].enemyChance.Evaluate(Random.value);
            }
            _waveHazards[i].index = GetRandomIndex(probabilities);
            _waveHazards[i].enemyType = _currentTypeOrder[_waveHazards[i].index];
            _waveHazards[i].key = _waveHazards[i].enemyType.ToString();
            _waveHazards[i].spawnPosition = GetRandomLocationOnCircle(spawnDistanceFromPlanet.Evaluate(Random.value));
        }
        _waveStartMoment = Time.time;
    }

    void Awake() {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);
    }

    void Start() {
        
    }

    void Update() {
        if (_currenHazard < _waveHazards.Length) {
            if (Time.time > _waveStartMoment + _waveTimeBreather) {
                if (Time.time > _waveStartMoment + _waveTimeBreather + _currenHazard * _waveTimeLength/_numberOfHazards) {
                    GameObject currentHazard = Instantiate(spawnableHazards[_waveHazards[_currenHazard].index].gameObject, _waveHazards[_currenHazard].spawnPosition, Quaternion.identity);
                    currentHazard.GetComponent<Hazard>().points = HazardWaypointManager.current.GetRandomPath().waypoints;
                    _currenHazard++;
                }
            }
        } else {
            GenerateWaveEnemies();
        }
    }

    void OnValidate() {
        for (int i = 0; i < enemyTypeDistribution.Length; i++) {
            enemyTypeDistribution[i].key = enemyTypeDistribution[i].enemyType.ToString();
        }
        for (int i = 0; i < initialWaves.Length; i++) {
            initialWaves[i].key = "Wave " + i;
            if (initialWaves[i].mode == WaveMode.Random) {
                //initialWaves[i].waveTimeLength = timePerWave;
                //initialWaves[i].enemyTypeDistribution = enemyTypeDistribution;
            }
        }
    }
}
