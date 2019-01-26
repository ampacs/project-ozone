using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSuicide : Hazard {
    void OnValidate() {
        type = Type.Suicide;
    }
}
