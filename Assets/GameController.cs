﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameController : MonoBehaviour {




    // Use this for initialization
    GameObject cam;
    GameObject[] players;

    void Start()
    {

        players = GameObject.FindGameObjectsWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        setCameraPreset(1);
        
    }
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            setCameraPreset(1);
        } else if (Input.GetKeyDown("2")){
            setCameraPreset(2);
        } else if (Input.GetKeyDown("3"))
        {
            setCameraPreset(3);
        }
    }
    public void setCameraPreset(int x)
    {
        /*
           1: Wide view of entire map
           2: StartSpace
           3:

        */
        cam.GetComponentInParent<CamBehavior>().setFollowPlayer(false);
        if(x == 1){
            cam.GetComponentInParent<CamBehavior>().setTargetLocation(new Vector3(300, 200, 275));
        } else if(x == 2)
        {
            cam.GetComponentInParent<CamBehavior>().setTargetLocation(new Vector3(500, 80, 0));
        } else if(x == 3)
        {
            GameObject starSpace = GameObject.FindGameObjectWithTag("StarSpace");
            cam.GetComponentInParent<CamBehavior>().setTargetLocation(new Vector3(starSpace.transform.position.x, 80, starSpace.transform.position.z));
        }
    }
    public void followPlayer(GameObject player)
    {
        cam.GetComponentInParent<CamBehavior>().setFollowPlayer(true);
        cam.GetComponentInParent<CamBehavior>().setTargetPlayer(player);
    }
    public void runPlayerTurns()
    {

    }
    public void getInitiative()
    {

    }

    public void End()
    {
       
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("GameBoard");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
