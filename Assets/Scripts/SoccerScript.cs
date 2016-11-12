using UnityEngine;
using System.Collections;

public class SoccerScript : MonoBehaviour {

    private Player[] leftTeam, rightTeam,win;
    private GameObject scoreDisplay;
    private Vector3 leftBound, rightBound;
    private int result = 0,leftScore=0,rightScore=0,currentPoint=1;
    private Rigidbody rb;
    public Material gray,red,blue;
    //result: 0 for playing, -1 for left side win, 1 for right side win

    // Use this for initialization

    void Start()
    {
        scoreDisplay = GameObject.Find("Point1");
        leftBound = new Vector3(-21, 0, 0);
        rightBound = new Vector3(21, 0, 0);
        rb = GetComponent<Rigidbody>();
        win = null;
        for (int i = 1; i <= 5; i++)
        {
            scoreDisplay = GameObject.Find("Point" + i.ToString());
            scoreDisplay.GetComponent<Renderer>().material = gray;
        }
        for(int i=0;i<4;i++)
        {
        }
        leftTeam[0].transform.position = new Vector3(-5,0,-2);
        leftTeam[1].transform.position = new Vector3(-5,0,2);
        rightTeam[0].transform.position = new Vector3(5,0,-2);
        rightTeam[1].transform.position = new Vector3(5,0,2);
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
            }
        }
    }

    void scorePoint(float pos)
    {
        if (pos>0)
        {
            rightScore++;
            scoreDisplay.GetComponent<Renderer>().material = blue;
        }
        else if(pos<0)
        {
            leftScore++;
            scoreDisplay.GetComponent<Renderer>().material = red;
        }
        currentPoint++;
        scoreDisplay = GameObject.Find("Point" + currentPoint.ToString());
        this.transform.position = new Vector3(0, 50, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }

    void onTriggerEnter(Collider c)
    {
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

    public Player[] giveResults()
    {
        return win;
    }
}
