using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameStateControl : MonoBehaviour {

    // Use this for initialization
    public GameObject startScreen;
    public GameObject endScreen;
    GameData gameData;
    private bool gameOver;
    private bool gameStart;
    GameObject[] playerList;
    public GameObject[] results;
    private bool[] playersReady;
    private bool allowPlayersReady;
	void Start () {
        gameOver = false;
        gameStart = false;
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        playersReady = new bool[4];
        for(int i = 0; i < playersReady.Length; i++)
        {
            playersReady[i] = false;
        }
        allowPlayersReady = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(onePlayerReady() && !gameStart)
        {
            startGame();
        }
        if(allPlayersReady() && gameOver)
        {
            SceneManager.LoadScene("GameBoard");
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
            SceneManager.LoadScene("GameBoard");
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

        } else
        {
            endScreen.GetComponent<UIRevealer>().hideUI();
        }
        for (int i = 0; i < playersReady.Length; i++)
        {
            playersReady[i] = false;
        }
        allowPlayersReady = true;
    }
    public void startGame()
    {
        gameStart = true;
        startScreen.GetComponent<UIRevealer>().hideUI();
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
    public bool playersCanReady()
    {
        return allowPlayersReady;
    }
    public bool allPlayersReady()
    {
        if (allowPlayersReady)
        {
            bool result = true;
            for (int i = 0; i < playersReady.Length; i++)
            {
                if (!playersReady[i])
                {
                    result = false;
                }
            }
            if (result)
            {
                //sets all back to unready and prevents them from readying until allowed again

                for (int i = 0; i < playersReady.Length; i++)
                {
                    playersReady[i] = false;
                }
                allowPlayersReady = false;
            }
            return result;

        } else
        {
            return false;
        }
        
    }
    public bool onePlayerReady()
    {
        bool result = false;

        if (allowPlayersReady)
        {
            for (int i = 0; i < playersReady.Length; i++)
            {
                if (playersReady[i])
                {
                    result = true;
                }
            }
        }
        return result;

    }
    public void playerIsReady(int x)
    {
        if (allowPlayersReady)
        {
            playersReady[x] = true;
        }
    }
}
