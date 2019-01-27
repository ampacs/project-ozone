using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour {
    public float lifetime = 0.3333333f;
    float _shootMoment;
    LineRenderer lineRenderer;

    public void Shoot (int damage) {
        lineRenderer.enabled = true;
        _shootMoment = Time.time;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider) {
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.InverseTransformPoint(hit.point));
                Damageable damageable = hit.collider.gameObject.transform.parent.GetComponent<Damageable>();
                if (damageable != null) {
                    damageable.Damage(damage);
                }
            }
        } else lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.InverseTransformPoint(transform.forward) * 50);
    }

    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
    }
    
    // Update is called once per frame
    void Update () {
        //lineRenderer.SetPosition(0, transform.position);
        if (Time.time > _shootMoment + lifetime)
            lineRenderer.enabled = false;
    }
}
