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
	void Start () {
        gameOver = false;
        gameStart = false;
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        print("gameData is type: " + gameData.GetType());

	}
	
	// Update is called once per frame
	void Update () {
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
                //print(playerRanks[i]);
                results[i].GetComponent<GetResultsStats>().setPlayerNum(playerRanks[i]);
            }
            endScreen.GetComponent<UIRevealer>().revealUI();

        } else
        {
            endScreen.GetComponent<UIRevealer>().hideUI();
        }
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
    public int numAlivePlayers()
    {
        return playerList.Length;   
    }
    public int[] getPlayerRanks()
    {
        List<int> scores = new List<int>();
        int[] result = new int[4];
        for(int i = 0; i < 4; i++)
        {
            scores.Add(getScore(i));
        }
        for (int i = 0; i < 4; i++)
        {
            int max = -1;
            int index = 0;
            for (int j = scores.Count-1; j >= 0; j--)
            {
                if (Mathf.Max(max, scores[j]) > max)
                {
                    max = scores[j];
                    index = j;

                }
            }
            print(index);
            result[i] = index;
            scores.RemoveAt(index);
        }

        return result;
    }
    public int getScore(int playerNum)
    {
        return gameData.getStars(playerNum) * 1000 + gameData.getCoins(playerNum);
    }
}
