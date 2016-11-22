using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class endGameController : MonoBehaviour {

    // Use this for initialization
    public UIRevealer resultsScreen;
    private GameData gameData;
    public GetResultsStats player1Stats;
    public GetResultsStats player2Stats;
    public GetResultsStats player3Stats;
    public GetResultsStats player4Stats;
    public Image player1CheckMark;
    public Image player2CheckMark;
    public Image player3CheckMark;
    public Image player4CheckMark;
    public MeshRenderer playerAModel;
    public MeshRenderer playerBModel;
    public MeshRenderer playerCModel;
    public MeshRenderer playerDModel;
    private MeshRenderer[] modelList;
    public Material player1Material;
    public Material player2Material;
    public Material player3Material;
    public Material player4Material;
    private Material[] materialList;
    private Image[] checkList;
    private GetResultsStats[] stats;
    private bool[] playersReady;
    private bool allowPlayersReady;
    private float timer;
    public Text winnerText;
	void Start () {
        timer = 6f;
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        stats = new GetResultsStats[4];
        stats[0] = player1Stats;
        stats[1] = player2Stats;
        stats[2] = player3Stats;
        stats[3] = player4Stats;
        playersReady = new bool[4];
        playersReady[0] = false;
        playersReady[1] = false;
        playersReady[2] = false;
        playersReady[3] = false;
        allowPlayersReady = false;
        checkList = new Image[4];
        checkList[0] = player1CheckMark;
        checkList[1] = player2CheckMark;
        checkList[2] = player3CheckMark;
        checkList[3] = player4CheckMark;
        modelList = new MeshRenderer[4];
        modelList[0] = playerAModel;
        modelList[1] = playerBModel;
        modelList[2] = playerCModel;
        modelList[3] = playerDModel;
        materialList = new Material[4];
        materialList[0] = player1Material;
        materialList[1] = player2Material;
        materialList[2] = player3Material;
        materialList[3] = player4Material;
        int[] ranks = getPlayerRanks();
        for(int i = 0; i < ranks.Length; i++)
        {
            modelList[i].material = materialList[ranks[i]];
        }
    }

    // Update is called once per frame
    void Update () {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }else
        {
            
            allowPlayersReady = true;
            int[] ranks = getPlayerRanks();
            for(int i = 0; i < 4; i++)
            {
                stats[i].setPlayerNum(ranks[i]);
                stats[i].updateData();
            }
            winnerText.text = "Player " + (ranks[0] + 1) + " is the Winner!!!";
            resultsScreen.revealUI();
            //Keyboard controls for debug
            if (Input.GetKeyDown("v"))
            {
                playerIsReady(0);
            }
            if (Input.GetKeyDown("b"))
            {
                playerIsReady(1);
            }
            if (Input.GetKeyDown("n"))
            {
                playerIsReady(2);
            }
            if (Input.GetKeyDown("m"))
            {
                playerIsReady(3);
            }
            //end keyboard controls

            if (Input.GetButtonDown("P1_Fire1"))
            {
                playerIsReady(0);
            }
            if (Input.GetButtonDown("P2_Fire1"))
            {
                playerIsReady(1);
            }
            if (Input.GetButtonDown("P3_Fire1"))
            {
                playerIsReady(2);
            }
            if (Input.GetButtonDown("P4_Fire1"))
            {
                playerIsReady(3);
            }
            if (allPlayersReady())
            {
                try {
                    GameObject.Find("TurnCounter").GetComponent<TurnCounter>().setTurnCount(10);
                }
                catch
                {
                    print("No turn counter found");
                }
                GameObject.DestroyObject(GameObject.FindGameObjectWithTag("GameData"));
                SceneManager.LoadScene("start menu");
                }
        }
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
        for (int i = 0; i < 4; i++)
        {
            result[i] = scores[i][1];
        }

        return result;
    }
    public int getScore(int playerNum)
    {
        return gameData.getStars(playerNum) * 1000 + gameData.getCoins(playerNum);
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

        }
        else
        {
            return false;
        }

    }
    public void playerIsReady(int x)
    {
        if (allowPlayersReady)
        {
            playersReady[x] = true;
            checkList[x].enabled = true;
        }
    }
}
