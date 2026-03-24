using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] PlayerMove _PlayerMove;
    [SerializeField] private int num;

    void Start()
    {
        
    }

    void Update()
    {
        if (_PlayerMove.life < num)
        {
            this.gameObject.SetActive(false);
        }
    }
}
