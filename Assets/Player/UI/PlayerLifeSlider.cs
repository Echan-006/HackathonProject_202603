using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeSlider : MonoBehaviour
{
    [SerializeField] Slider _Slider;
    [SerializeField] PlayerMove _PlayerMove;

    void Start()
    {
        
    }

    void Update()
    {
        _Slider.value = (float)_PlayerMove.life / _PlayerMove.lifeMax;
    }
}
