using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] Enemy _Enemy;
    [SerializeField] private int num;

    void Start()
    {
        
    }

    void Update()
    {
        if (_Enemy.life < num)
        {
            this.gameObject.SetActive(false);
        }
    }
}
