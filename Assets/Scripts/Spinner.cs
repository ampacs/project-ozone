using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

    public enum Axis {
        up, down, right, left, forward, backward, custom
    }
    public Axis rotationAxis;
    public Vector3 _rotationAxis;
    public float speed = 10f;

    Vector3 GetRotationAxis (Axis axis) {
        switch (axis) {
            case Axis.up: return Vector3.up;
            case Axis.down: return -Vector3.up;
            case Axis.right: return Vector3.right;
            case Axis.left: return -Vector3.right;
            case Axis.forward: return Vector3.forward;
            case Axis.backward: return -Vector3.forward;
            default: break;
        }
        return new Vector3(-1, -1, -1);
    }

    void Update() {
        transform.Rotate(_rotationAxis, speed * Time.deltaTime, Space.World);
    }

    void OnValidate() {
        Vector3 previousAxis = _rotationAxis;
        _rotationAxis = GetRotationAxis(rotationAxis);
        if (_rotationAxis == new Vector3(-1, -1, -1)) {
            _rotationAxis = previousAxis;
        }
    }
}