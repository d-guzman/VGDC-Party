using UnityEngine;
using System.Collections;

public class SoccerScript : MonoBehaviour {

    private Player[] leftTeam, rightTeam;
    private GameObject scoreDisplay;
    private Vector3 leftBound, rightBound;
    private int result = 0,leftScore=0,rightScore=0,currentPoint=1;
    private Rigidbody rb;
    public Material gray,red,blue;
    //0 for playing, -1 for left side win, 1 for right side win

    // Use this for initialization

    void Start()
    {
        scoreDisplay = GameObject.Find("Point1");
        leftBound = new Vector3(-21, 0, 0);
        rightBound = new Vector3(21, 0, 0);
        rb = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        for (int i = 1; i <= 5; i++)
        {
           // scoreDisplay[i].GetComponent<Renderer>().material = gray;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < leftBound.x)
        {
            leftScore++;
            scoreDisplay.GetComponent<Renderer>().material = red;
            currentPoint++;
            scoreDisplay = GameObject.Find("Point"+currentPoint.ToString());
            this.transform.position = new Vector3(0, 50, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
        if (this.transform.position.x > rightBound.x)
        {
            rightScore++;
            scoreDisplay.GetComponent<Renderer>().material = blue;
            currentPoint++;
            scoreDisplay = GameObject.Find("Point" + currentPoint.ToString());
            this.transform.position = new Vector3(0, 50, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
    void onTriggerEnter(Collider c)
    {
    }
    public int getResult() { return result; }
    public Player[] getWinner(int result)
    {
        if (result == 1) { return rightTeam; }
        else { return leftTeam; }
    }
}
