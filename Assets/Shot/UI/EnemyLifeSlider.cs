using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeSlider : MonoBehaviour
{
    [SerializeField] Slider _Slider;
    [SerializeField] Enemy _Enemy;

    void Start()
    {
        
    }

    void Update()
    {
        _Slider.value = (float)_Enemy.life / _Enemy.lifeMax;
    }
}
