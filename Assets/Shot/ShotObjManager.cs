using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotObjManager : MonoBehaviour
{
    public ShotPool _ShotPool;
    public MonoBehaviour[] Scripts;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ReleaseObj()
    {
        _ShotPool.ShotObjPool.Release(this.gameObject);
    }
}
