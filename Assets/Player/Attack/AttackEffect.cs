using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public AttackEffectPool _AttackEffectPool;
    [SerializeField] ParticleSystem _ParticleSystem;

    void Start()
    {
        
    }

    void Update()
    {
        if (!_ParticleSystem.IsAlive(true))
        {
            _AttackEffectPool.EffectPool.Release(this.gameObject);
        }
    }
}
