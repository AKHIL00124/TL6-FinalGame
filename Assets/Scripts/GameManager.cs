using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    [SerializeField]
    HealthBar hb;

    public static GameManager Instance { get; private set; }
    
    //If a script will be using the singleton in its awake method, make sure the manager is first to execute with the Script Execution Order project settings
    void Awake(){
        if (Instance != null) //this depend how you want to handle multiple managers (like when switching/adding scenes) but this way should cover common use cases
            Destroy(Instance);
        Instance = this;
    }
    
    void OnDestroy(){
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void take_damage(){
        hb.subtract_damage();
    }

    public void game_over(){
        SceneManager.LoadScene(3);
        // Time.timeScale = 0;
        //trigger the game over screen...
    }

}
