using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameController : MonoBehaviour {




    // Use this for initialization
    GameObject cam;
    GameObject[] players;
    GameObject currentPlayer;
    GameObject turnCounter;
    LowerScreenTextScript lowerScreenUI;

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

    private int playerTurn;

    private int minigameType;

    public string[] minigamesFFA;     //scene names go in here
    public string[] minigames2v2;   //scene names go in here
    public string[] minigames1v3;   //scene names go in here

    private string[][] minigameList;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Player>().setListOfPlayers(players);
        }
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        setCameraPreset(1);
        playerTurn = 0;
        boardState = GET_INITIATIVE;
        gameState = GAME_BOARD;
        
        minigameList = new string[3][] { minigamesFFA, minigames2v2, minigames1v3 };
        turnCounter = GameObject.Find("TurnCounter");
        setPlayerRanks();
        lowerScreenUI = GameObject.Find("LowerScreen").GetComponent<LowerScreenTextScript>();
        
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

        


        //game flow is under this

        if(gameState == 1)
        {
            //print(boardState);
            
            if(boardState == PRE_GAME)
            {
                
            } else if(boardState == GET_INITIATIVE)
            {
                int rollingPlayer = -1;
              
                for(int j = 0; j < players.Length; j++)
                {
                    if (!players[j].GetComponent<Player>().getInitiative())
                    {
                        rollingPlayer = j;
                        j = 100000; //break out of loop
                    }
                    
                }
                if(rollingPlayer != -1)
                {
                    followPlayer(players[rollingPlayer]);
                    players[rollingPlayer].GetComponent<Player>().setPlayerState(5);
                } else
                {
                    setTurnOrder();
                    
                    setBoardState(NEW_TURN);
                }
            } else if(boardState == NEW_TURN)
            {
                //display turns left and other stuff at the beginning of each round of turns.
                setBoardState(PLAYERS_TURN);
            } else if(boardState == PLAYERS_TURN)
            {
               
                if(playerTurn < players.Length)
                {
                    currentPlayer = getCurrentPlayer(playerTurn);
                   
                    if (currentPlayer.GetComponent<Player>().getState() == 0)
                    {
                        currentPlayer.GetComponent<Player>().setPlayerState(1);
                        followPlayer(currentPlayer);
                    }
                    else if (currentPlayer.GetComponent<Player>().getState() == 6)
                    {
                        currentPlayer.GetComponent<Player>().setPlayerState(0);
                        playerTurn++;
                    }
                } else
                {
                    turnCounter.GetComponent<TurnCounter>().decrementTurnCount();
                    setBoardState(DECIDE_MINIGAME);
                }

            } else if(boardState == DECIDE_MINIGAME)
            {
                startMinigame(minigameType);
            }
        }
    }
    public void setCameraPreset(int x)
    {
        /*
           1: Wide view of entire map
           2: StartSpace
           3: StarSpace

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
    public int getMinigameType()
    {
        int result = -1;
        int redCount = 0;
        int blueCount = 0;
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].GetComponent<Player>().getSpaceType() == 0)
            {
                blueCount++;
            } else if(players[i].GetComponent<Player>().getSpaceType() == 1)
            {
                redCount++;
            }
            else
            {
                print("player on unknown space type");
            }
        }
        if(blueCount == 4 || redCount == 4)
        {
            result = 0;   //FFA
        } else if(blueCount == 3 || redCount == 3)
        {
            result = 1;  //1v3
        } else if(blueCount == 2 || redCount == 2)
        {
            result = 2; //2v2
        } else
        {
            print("Unusual amount of blue and red spaces");
        }
        return result;
    }
    public void startMinigame(int type)
    {
        setGameState(MINIGAME);
        
        int rngGame = Random.Range(0, minigameList[type].Length);

        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetActive(false);
        }

        //SceneManager.LoadScene(minigameList[type][rngGame]);     //this is correct implementation, but requires at least 1 of every game type
        SceneManager.LoadScene("mini_arena");

    }
    public void loadGameBoard()
    {
        gameState = GAME_BOARD;
        SceneManager.LoadScene("GameBoard");
        players = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Player>().setListOfPlayers(players);
        }
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
    public void setGameState(int x)
    {
        //this code runs once whenever a state is changed
        //use this for initializing values for a game state
        //so you don't need to keep recalculating certain things or repeating actions
        //USE THIS CONSISTENTLY PLEASE
        gameState = x;
        if(gameState == GAME_BOARD)
        {
            loadGameBoard();
        }
    }
    public void setBoardState(int x)
    {
        //this code runs once whenever a state is changed
        //use this for initializing values for a board state
        //so you don't need to keep recalculating certain things or repeating actions
        //USE THIS CONSISTENTLY PLEASE
        boardState = x;
        if(boardState == PRE_GAME)
        {
            setCameraPreset(1);
        } else if(boardState == GET_INITIATIVE)
        {
            setCameraPreset(2);
        } else if(boardState == NEW_TURN)
        {

        } else if(boardState == PLAYERS_TURN)
        {
            playerTurn = 0;
        } else if(boardState == DECIDE_MINIGAME)
        {
            minigameType = getMinigameType();
           
        }
    }
    public void setTurnOrder()
    {
        
        for(int i = 0; i < players.Length; i++)
        {
            int max = 0;
            int index = 0;
            for(int j = 0; j < players.Length; j++)
            {
                if(max < players[j].GetComponent<Player>().getInitativeNum() && players[j].GetComponent<Player>().getTurnOrder() == -1)
                {
                    max = players[j].GetComponent<Player>().getInitativeNum();
                    index = j;
                }
            }
            players[index].GetComponent<Player>().setTurnOrder(i);
        }
        
    }
    public GameObject getCurrentPlayer(int x)
    {
        GameObject result = null;
        for(int i = 0; i < 4; i++)
        {
            if (players[i].GetComponent<Player>().getTurnOrder() == x)
            {
                result = players[i];
            }
        }
        return result;
    }
    public void setPlayerRanks()
    {
        for(int i = 0; i < players.Length; i++)
        {
            int max = 0;
            int index = 0;
            for(int j = i; j < players.Length; j++)
            {
                if(Mathf.Max(max,players[j].GetComponent<Player>().getScore()) > max)
                {
                    max = players[j].GetComponent<Player>().getScore();
                    index = j;
                }
            }
            GameObject temp = players[0];
            players[0] = players[index];
            players[index] = temp;

        }
        for(int i = 0; i < players.Length; i++)
        {
            if(i != 0 && players[i-1].GetComponent<Player>().getScore() == players[i].GetComponent<Player>().getScore())
            {
                players[i].GetComponent<Player>().setRank(players[i - 1].GetComponent<Player>().getRank());
            }else
            {
                players[i].GetComponent<Player>().setRank(i);

            }
        }


    }
}
