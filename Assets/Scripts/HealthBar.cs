using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider healthSlider;
    private GameManager gm;

    private int maxHealth;

    private void Start(){
        if(gm == null){
            gm = GameManager.Instance;
        }
        healthSlider = GetComponent<Slider>();

        maxHealth = 3;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void SetMaxHealth(int maxHealth){
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void subtract_damage(){
        healthSlider.value -= 1;
        if(healthSlider.value <= 0){
            gm.game_over();
        }
    }

    // public void Death(){
    //     healthSlider.value = 0;
    //     Time.timeScale = 0;
    //     //trigger the game over screen...
    // }
}
