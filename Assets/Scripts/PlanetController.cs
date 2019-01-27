using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : Damageable {
    
    public static PlanetController current;
    public float regenerationRate = 0.5f;
    public Color healthyTerrainColor;
    public Color deadTerrainColor;

    public Color healthyWaterColor;
    public Color deadWaterColor;

    float _lastRegenMoment;
    Color _currentTerrainColor;
    Color _currentWaterColor;
    MeshRenderer _meshRenderer;

    public override void Damage(int damage) {
        health -= damage;
        _currentWaterColor = Color.Lerp(deadWaterColor, healthyWaterColor, health/(float)maxHealth);
        _currentTerrainColor = Color.Lerp(deadTerrainColor, healthyTerrainColor, health/(float)maxHealth);
        _meshRenderer.materials[0].SetColor("_Color", _currentWaterColor);
        _meshRenderer.materials[1].SetColor("_Color", _currentTerrainColor);
    }

    public override void Kill() {

    }

    void Awake() {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);
    }

    void Start() {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Time.time > _lastRegenMoment + regenerationRate) {
            _lastRegenMoment = Time.time + (Time.time - _lastRegenMoment - regenerationRate);
            Damage(-1);
            if (health > maxHealth) {
                health = maxHealth;
            }
        }
    }
}
