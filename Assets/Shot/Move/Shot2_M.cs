using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot2_M : MonoBehaviour
{
    public Shot_M_Straight _Shot_M_Straight;

    void OnEnable()
    {
        _Shot_M_Straight.enabled = false;
    }

    void Update()
    {
        
    }
}
