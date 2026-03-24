using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpNova : MonoBehaviour
{
    public PlayerMove _PlayerMove;
    [SerializeField] ParticleSystem _ParticleSystem;

    void Start()
    {
        
    }

    void Update()
    {
        if (!_ParticleSystem.IsAlive(true))
        {
            _PlayerMove.NovaPool.Release(this.gameObject);
        }
    }
}
