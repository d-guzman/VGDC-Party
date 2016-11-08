using UnityEngine;
using System.Collections;

public class SoccerScript : MonoBehaviour {

    private Player[] leftTeam, rightTeam;
    private Vector3 leftBound, rightBound;
    private int result = 0,leftScore=0,rightScore=0;
    private Rigidbody rb;
    //0 for playing, -1 for left side win, 1 for right side win

    // Use this for initialization
	void Start () {
        leftBound = new Vector3(-21, 0, 0);
        rightBound = new Vector3(21, 0, 0);
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < leftBound.x)
        {
            leftScore++;
            this.transform.position = new Vector3(0, 50, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
        if (this.transform.position.x > rightBound.x)
        {
            rightScore++;
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
