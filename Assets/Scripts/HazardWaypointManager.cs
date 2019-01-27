using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardWaypointManager : MonoBehaviour {

    public static HazardWaypointManager current;

    public Path[] paths;

    public Path GetExitPath (Vector3 location) {
        Path path = new Path();
        path.waypoints = new Transform[] {new GameObject().transform};
        path.waypoints[0].position = (location - PlanetController.current.transform.position).normalized * 10f;
        return path;
    }

    public Path GetRandomPath () {
        return paths[Random.Range(0, paths.Length)];
    }

    public Path GetRandomLoopablePath () {
        Path path;
        do {
            path = paths[Random.Range(0, paths.Length)];
        } while (!path.loopable);
        return path;
    }

    public Path GetRandomNonLoopablePath () {
        Path path;
        do {
            path = paths[Random.Range(0, paths.Length)];
        } while (path.loopable);
        return path;
    }

    void Awake() {
        if (current == null)
            current = this;
        else if (current != this)
            Destroy(gameObject);
    }
}
