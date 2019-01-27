using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public new string name;
    public bool loopable;
    public Transform[] waypoints;

    public int GetNextIndex(int index) {
        if (++index >= waypoints.Length) {
            if (loopable)
                return 0;
            else return index--;
        }
        return index;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
