using UnityEngine;
using System.Collections;

public class SoccerScript : MonoBehaviour {

    private GameObject[] leftTeam, rightTeam,win;
    private GameObject scoreDisplay;
    private Vector3 leftBound, rightBound;
    private int result = 0,leftScore=0,rightScore=0,currentPoint=1;
    private bool started;
    private Rigidbody rb;
    public Material gray,red,blue;
    //result: 0 for playing, -1 for left side win, 1 for right side win

    // Use this for initialization

    void Start()
    {
        leftBound = new Vector3(-21, 0, 0);
        rightBound = new Vector3(21, 0, 0);
        rb = GetComponent<Rigidbody>();
        leftTeam = new GameObject[2];
        rightTeam = new GameObject[2];
        started = false;
        win = null;
        for (int i = 1; i <= 5; i++)
        {
            scoreDisplay = GameObject.Find("Point" + i.ToString());
            scoreDisplay.GetComponent<Renderer>().material = gray;
        }
        scoreDisplay = GameObject.Find("Point1");
//        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        int l = 0;
        int r = 0;
        //for (int i=0;i<4;i++)
        //{
        //    //Assumes 2 red, 2 blue
        //    if (a[i].GetComponent<Player>().getSpaceType() == 0)
        //    {
        //        leftTeam[l] = GameObject.Find("Player"+i.ToString());
        //        GameObject.find("P"+i.ToString()+"_Model").GetComponent<Renderer>().material=blue;
        //        l++;
        //    }
        //    else
        //    {
        //        rightTeam[r] = GameObject.Find("Player" + i.ToString());
        //        GameObject.find("P"+i.ToString()+"_Model").GetComponent<Renderer>().material=red;
        //        r++;
        //    }
        //}
        //temporary implementation
        leftTeam[0] = GameObject.Find("Player1");
        leftTeam[1] = GameObject.Find("Player2");
        rightTeam[0] = GameObject.Find("Player3");
        rightTeam[1] = GameObject.Find("Player4");
        leftTeam[0].transform.position = new Vector3(-5,3,-3);
        leftTeam[1].transform.position = new Vector3(-5,3,3);
        rightTeam[0].transform.position = new Vector3(5,3,-3);
        rightTeam[1].transform.position = new Vector3(5,3,3);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < leftBound.x || this.transform.position.x > rightBound.x)
        {
            scorePoint(this.transform.position.x);
            checkWin(leftScore, rightScore);
            if (result!=0)
            {
                setWinner(result);
                //results board stuff, coin changes
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
        if (result == 1) { win = rightTeam; }
        else if (result == -1) { win = leftTeam; }
    }

    public int getResult() { return result; }

    public Object[] giveResults()
    {
        return win;
    }
}
