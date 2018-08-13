using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZConstraint : MonoBehaviour {
    private void LateUpdate()
    {
        var pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
}
