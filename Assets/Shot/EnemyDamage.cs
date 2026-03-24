using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public Enemy _Enemy;
    [SerializeField] ParticleSystem _ParticleSystem;

    void Start()
    {
        
    }

    void Update()
    {
        if (!_ParticleSystem.IsAlive(true))
        {
            _Enemy.DamagePool.Release(this.gameObject);
        }
    }
}
