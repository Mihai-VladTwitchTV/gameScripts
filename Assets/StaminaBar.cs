using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    float cap;
    float quant;
    public Slider stamBar;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        cap = player.staminaCap;
        quant = cap;
        stamBar.minValue = 0f;
        stamBar.maxValue = player.staminaCap;
    }

    // Update is called once per frame
    void Update()
    {
        stamBar.value = player.staminaQuant;
        
    }
}
