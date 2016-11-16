using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameStateControl : MonoBehaviour {

    // Use this for initialization
    public GameObject startScreen;
    public GameObject endScreen;
    GameData gameData;
    private bool gameOver;
    private bool gameStart;
    GameObject[] playerList;

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
}
