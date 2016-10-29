using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameController : MonoBehaviour {




    // Use this for initialization
    GameObject cam;
    GameObject[] players;
    int gameState;
    private const int MAIN_MENU = 0;
    private const int GAME_BOARD = 1;
    private const int MINIGAME = 2;
    private const int GAME_OVER = 3;

    int boardState;
    private const int PRE_GAME = 0;
    private const int GET_INITIATIVE = 1;
    private const int NEW_TURN = 2;
    private const int PLAYERS_TURN = 3;
    private const int DECIDE_MINIGAME = 4;
    
    
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
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
        } else if (Input.GetKeyDown("4"))
        {
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<Player>().isActiveAndEnabled)
                {
                    followPlayer(players[i]);
                }
            }
        }


        //game flow is under this

        if(gameState == 1)
        {
            if(boardState == PRE_GAME)
            {
                setCameraPreset(1);
            } else if(boardState == GET_INITIATIVE)
            {
                int i = -1;
              
                for(int j = 0; j < players.Length; i++)
                {
                    /*
                    if (!players[j].GetComponent<Player>().hasInitiative())
                    {
                        i = j;
                        j = 100000; //break out of loop
                    }
                    */
                }
                if(i != -1)
                {
                    followPlayer(players[i]);
                    //players[i].GetComponent<Player>().setPlayerState(some_num);
                } else
                {
                    boardState = NEW_TURN;
                }
            } else if(boardState == NEW_TURN)
            {

            } else if(boardState == PLAYERS_TURN)
            {

            } else if(boardState == DECIDE_MINIGAME)
            {

            }
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
    public void start_minigame(int x)
    {
        gameState = MINIGAME;
        if(x == 0)
        {
            for(int i = 0; i < players.Length; i++)
            {
                players[i].SetActive(false);
            }
            SceneManager.LoadScene("mini_arena");
        }

    }
    public void loadGameBoard()
    {
        gameState = GAME_BOARD;
        SceneManager.LoadScene("GameBoard");
        players = GameObject.FindGameObjectsWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        setCameraPreset(1);

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
        gameState = MAIN_MENU;
        SceneManager.LoadScene("MainMenu");

    }
}
