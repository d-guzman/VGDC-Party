using UnityEngine;
using System.Collections;

public class SoccerScript : MonoBehaviour
{

    private GameObject[] leftTeam, rightTeam;
    private GameObject scoreDisplay,events;
    public GameObject control;
    private Vector3 leftBound, rightBound;
    private int result = 0, leftScore = 0, rightScore = 0, currentPoint = 1;
    private int[] redP, blueP,win;
    private bool started;
    private Rigidbody rb;
    public Material gray, red, blue,yellow,white;
    public Material[] playerMat;
    //result: 0 for playing, -1 for left side win, 1 for right side win

    // Use this for initialization

    void Start()
    {
        events = GameObject.FindGameObjectWithTag("GameData");
        //control = GameObject.Find("MiniGameController");
        leftBound = new Vector3(-21, 0, 0);
        rightBound = new Vector3(21, 0, 0);
        rb = GetComponent<Rigidbody>();
        leftTeam = new GameObject[2];
        rightTeam = new GameObject[2];
        redP = new int[2];
        blueP = new int[2];
        started = false;
        win = null;
        playerMat = new Material[] { red, yellow, white, blue };
        string[] a = new string[4];
        for (int i = 1; i <= 5; i++)
        {
            scoreDisplay = GameObject.Find("Point" + i.ToString());
            scoreDisplay.GetComponent<Renderer>().material = gray;
        }
        scoreDisplay = GameObject.Find("Point1");
        for (int i = 0; i < 4; i++)
        {
            //a[i]=GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>().getCurrentSpaceTag(i);
            a[i] = events.GetComponent<GameData>().getCurrentSpaceTag(i);
        }
        int l = 0;
        int r = 0;
        if (a[0]!=null)
        {
            for (int i = 1; i <= 4; i++)
            {
                //Assumes 2 red, 2 blue
                if (a[i-1].Equals("BlueSpace"))
                {
                    blueP[l] = i;
                    //leftTeam[l] = GameObject.Find("Player" + i.ToString());
                    leftTeam[l] = GameObject.Find("P" + i.ToString()+"_Model");
                    //leftTeam[l].GetComponentInChildren<Renderer>().material = playerMat[i-1];
                    leftTeam[l].GetComponent<Renderer>().material = playerMat[i-1];
                    l++;
                }
                else if(a[i-1].Equals("RedSpace"))
                {
                    redP[r] = i;
                    //rightTeam[r] = GameObject.Find("Player" + i.ToString());
                    rightTeam[r] = GameObject.Find("P" + i.ToString()+"_Model");
                    //rightTeam[r].GetComponentInChildren<Renderer>().material = playerMat[i-1];
                    rightTeam[r].GetComponent<Renderer>().material = playerMat[i-1];
                    r++;
                }
            }
        }
        else
        {
            //temporary implementation
            leftTeam[l] = GameObject.Find("P1_Model");
            leftTeam[l].GetComponent<Renderer>().material = playerMat[l+r];
            l++;
            blueP[l] = 1;
            leftTeam[l] = GameObject.Find("P2_Model");
            leftTeam[l].GetComponent<Renderer>().material = playerMat[l + r];
            blueP[l] = 2;
            rightTeam[r] = GameObject.Find("P3_Model");
            rightTeam[r].GetComponent<Renderer>().material = playerMat[l + r+1];
            r++;
            redP[l] = 3;
            rightTeam[r] = GameObject.Find("P4_Model");
            rightTeam[r].GetComponent<Renderer>().material = playerMat[l + r+1];
            redP[l] = 4;
        }
        leftTeam[0].transform.position = new Vector3(-5, 3, -3);
        leftTeam[1].transform.position = new Vector3(-5, 3, 3);
        rightTeam[0].transform.position = new Vector3(5, 3, -3);
        rightTeam[1].transform.position = new Vector3(5, 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("SubmitStart"))
        {
            control.GetComponent<GameStateControl>().startGame();
            //first.
        }
        if (this.transform.position.x < leftBound.x || this.transform.position.x > rightBound.x)
        {
            scorePoint(this.transform.position.x);
            checkWin(leftScore, rightScore);
            if (result != 0 && !started)
            {
                setWinner(result);
                for (int i = 0; i < 2; i++)
                { events.GetComponent<GameData>().setCoins(win[i]-1, events.GetComponent<GameData>().getCoins(win[i]-1)+10); }
                print("game done");
                control.GetComponent<GameStateControl>().setGameOver(true);
                started = true;
            }
        }
    }

    void scorePoint(float pos)
    {
        if (rightScore + leftScore < 5)
        {
            if (pos > 0)
            {
                rightScore++;
                scoreDisplay.GetComponent<Renderer>().material = blue;
            }
            else if (pos < 0)
            {
                leftScore++;
                scoreDisplay.GetComponent<Renderer>().material = red;
            }
            currentPoint++;
            scoreDisplay = GameObject.Find("Point" + currentPoint.ToString());
        }
        this.transform.position = new Vector3(0, 50, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }

    void checkWin(int l, int r) //Assumes l is leftScore, r is rightScore
    {
        if (l == 3) { result = -1; }
        if (r == 3) { result = 1; }
    }

    void setWinner(int res)
    {
        if (result == 1) { win = redP; }
        else if (result == -1) { win = blueP; }
    }

    public int[] getWinner()
    {
        int[] ret = { win[0]-1,win[1]-1 };
        return ret;
    }
}
