using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameStateControl : MonoBehaviour {

    // Use this for initialization
    public GameObject startScreen;
    public GameObject endScreen;
    public UIRevealer blackPanel;
    GameData gameData;
    private bool gameOver;
    private bool gameStart;
    GameObject[] playerList;
    public GameObject[] results;
    
    public ReadyGate readyGate;
    float transitionTimer;
    bool loadScene;
	void Start () {
        gameOver = false;
        gameStart = false;
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        readyGate.allowReadying(true);
        transitionTimer = 0.5f;
        loadScene = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(readyGate.onePlayerReady() && !gameStart)
        {
            startGame();
        }
        if(readyGate.allPlayersReady() && gameOver)
        {
            loadScene = true;
            transitionTimer = 0.5f;
            blackPanel.revealUI();

        }
        if (Input.GetKeyDown("6") && !gameStart)
        {
            startGame();
        } else if (Input.GetKeyDown("6") && gameStart)
        {
            setGameOver(true);
        }
        if (Input.GetKeyDown("7"))
        {
            loadScene = true;
            transitionTimer = 0.5f;
            blackPanel.revealUI();
        }
        if (loadScene)
        {
            if(transitionTimer > 0)
            {
                transitionTimer -= Time.deltaTime;
            } else
            {
                SceneManager.LoadScene("GameBoard");

            }
        }
    }
    public void setGameOver(bool x)
    {
        gameOver = x;
        if (gameOver)
        {
            int[] playerRanks = getPlayerRanks();
            for(int i = 0; i < 4; i++)
            {
                results[i].GetComponent<GetResultsStats>().setPlayerNum(playerRanks[i]);
            }
            endScreen.GetComponent<UIRevealer>().revealUI();

            //int winningPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getPlayerNum();
            int winningPlayer = 0;
            results[winningPlayer].GetComponent<GetResultsStats>().addCoins(10);
        } else
        {
            endScreen.GetComponent<UIRevealer>().hideUI();
        }
        readyGate.unReadyAllPlayers();
        readyGate.allowReadying(true);
    }
    public void startGame()
    {
        gameStart = true;
        startScreen.GetComponent<UIRevealer>().hideUI();
        readyGate.unReadyAllPlayers();
        readyGate.allowReadying(false);
    }
    public bool getGameOver()
    {
        return gameOver;
    }
    public bool getGameStarted()
    {
        return gameStart;
    }
    public int numAlivePlayers()
    {
        return playerList.Length;   
    }
    public int[] getPlayerRanks()
    {
        //returns a list of player numbers in order with 0 being first and 3 being last
        //ex: [0,2,3,1]
        int[] result = new int[4];
        int[][] scores = new int[4][];

        for (int i = 0; i < 4; i++)
        {
            scores[i] = new int[2];
            scores[i][0] = getScore(i);
            scores[i][1] = i;
        }

        for (int i = 0; i < 4; i++)
        {
            int max = -1;
            int index = 0;
            for (int j = i; j < 4; j++)
            {
                if (Mathf.Max(max, scores[j][0]) > max)
                {
                    max = scores[j][0];
                    index = j;

                }
            }
            int[] thing = scores[index];
            scores[index] = scores[i];
            scores[i] = thing;
            
        }
        for(int i = 0; i < 4; i++)
        {
            result[i] = scores[i][1];
        }
        
        return result;
    }
    public int getScore(int playerNum)
    {
        return gameData.getStars(playerNum) * 1000 + gameData.getCoins(playerNum);
    }
    
    
    
}
