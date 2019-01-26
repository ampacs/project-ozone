using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public static PlayerMovementController current;

    public GameObject reference;
    // public float distance = 1f;
    public float topSpeed = 50f;
    public float accel = 10f;

    private float _currentSpeed;

    void Awake() {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);
    }

    void Start() {
        reference = GameObject.FindWithTag("Planet");
        _currentSpeed = 0f;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 relativePosition = (transform.position - reference.transform.position).normalized;
        Vector3 inputAxis = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 direction = -Vector3.Cross(relativePosition, Vector3.up);
        // Vector3 relativeInputAxis = Vector3.Scale(inputAxis, new Vector3(Vector3.Dot(direction, Vector3.right), 0f, Vector3.Dot(direction, Vector3.forward)));
        float relativeInputAxis = Vector3.Dot(inputAxis, direction);
        relativeInputAxis = relativeInputAxis > 0.866025403784f ? 1 : (relativeInputAxis < -0.866025403784f ? -1 : relativeInputAxis);  // 0.866025403784 ~ 30 degrees of leniency
        Debug.DrawRay(transform.position, direction);

        _currentSpeed = Mathf.Lerp(_currentSpeed, relativeInputAxis*topSpeed, accel * Time.fixedDeltaTime);

        transform.RotateAround(reference.transform.position, Vector3.up, _currentSpeed * Time.fixedDeltaTime);
    }
}
