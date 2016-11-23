using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    private WinnerList winnerList;
    //result: 0 for playing, -1 for left side win, 1 for right side win

    // Use this for initialization

    void Start()
    {
        events = GameObject.FindGameObjectWithTag("GameData");
        winnerList = GetComponent<WinnerList>();
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
        string[] tagData = new string[4];
        for (int i = 1; i <= 5; i++)
        {
            scoreDisplay = GameObject.Find("Point" + i.ToString());
            scoreDisplay.GetComponent<Renderer>().material = gray;
        }
        scoreDisplay = GameObject.Find("Point1");
        for (int i = 0; i < 4; i++)
        {
            //a[i]=GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>().getCurrentSpaceTag(i);
            tagData[i] = events.GetComponent<GameData>().getCurrentSpaceTag(i);
        }
        int l = 0;
        int r = 0;
        if (tagData[0]!=null)
        {
            for (int i = 0; i < 4; i++)
            {
                //Assumes 2 red, 2 blue
                if (tagData[i].Equals("BlueSpace"))
                {
                    blueP[l] = i;
                    //leftTeam[l] = GameObject.Find("Player" + i.ToString());
                    leftTeam[l] = GameObject.Find("P" + i.ToString()+"_Model");
                    //leftTeam[l].GetComponentInChildren<Renderer>().material = playerMat[i-1];
                    leftTeam[l].GetComponent<Renderer>().material = playerMat[i];
                    l++;
                }
                else if(tagData[i].Equals("RedSpace"))
                {
                    redP[r] = i;
                    //rightTeam[r] = GameObject.Find("Player" + i.ToString());
                    rightTeam[r] = GameObject.Find("P" + i.ToString()+"_Model");
                    //rightTeam[r].GetComponentInChildren<Renderer>().material = playerMat[i-1];
                    rightTeam[r].GetComponent<Renderer>().material = playerMat[i];
                    r++;
                }
            }
        }
        else
        {
            //temporary implementation
            leftTeam[0] = GameObject.Find("P1_Model");
            leftTeam[0].GetComponent<Renderer>().material = playerMat[0];
            l++; //<<< what the heck is this?
            blueP[0] = 0;
            leftTeam[1] = GameObject.Find("P2_Model");
            leftTeam[1].GetComponent<Renderer>().material = playerMat[1];
            blueP[1] = 1;
            rightTeam[0] = GameObject.Find("P3_Model");
            rightTeam[0].GetComponent<Renderer>().material = playerMat[2];
            r++;
            redP[0] = 2;
            rightTeam[1] = GameObject.Find("P4_Model");
            rightTeam[1].GetComponent<Renderer>().material = playerMat[3];
            redP[1] = 3;
            print(blueP[0]);
            print(blueP[1]);
            print(redP[0]);
            print(redP[1]);
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
            //checkWin(leftScore, rightScore);
            result = winningTeam(leftScore, rightScore);
            if (result != 0 && !started)
            {
                print("game done");
                List<int> temp = new List<int>();
                temp.Add(getWinner(result)[0]);
                temp.Add(getWinner(result)[1]);
                winnerList.setWinners(temp);
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
                leftScore++;
                scoreDisplay.GetComponent<Renderer>().material = blue;
            }
            else if (pos < 0)
            {
                rightScore++;
                scoreDisplay.GetComponent<Renderer>().material = red;
            }
            currentPoint++;
            scoreDisplay = GameObject.Find("Point" + currentPoint.ToString());
        }
        this.transform.position = new Vector3(0, 50, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }

   

    public int[] getWinner(int res)
    {
        if (result == 1) {
            return redP;
        }
        else if (result == -1)
        {
           return blueP;
        } else
        {
            return null;
        }
    }

   
    private int winningTeam(int lScore, int rScore)
    {
        if(lScore == 3)
        {
            return -1; //left wins
        } else if(rScore == 3)
        {
            return 1; //right wins
        } else
        {
            return 0; //no winner
        }
    }
}
