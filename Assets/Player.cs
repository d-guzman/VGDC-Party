using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    
    public string playerName;
    public int stars = 0,coins,highestCoins=0,mgWon=0,blueCount=0,redCount=0,rank=1,turnOrder,toMove;
    public bool active=false,rolling=false,isMoving=false,onEdge=false;
    private GameObject currentSpace=GameObject.Find("StartSpace");
    //Should coins just start at 10? What does the starting point count as spacewise?
    

	// Use this for initialization
	void Start () {
	
	}
    	
	// Update is called once per frame  
	void Update () {
        if (active)
        {
            if (onEdge)
                moveToEdge();
            if (rolling)
            {
                if (Input.GetButton("0"))
                {
                    toMove=Random.Range(1,6);
                }
            }
            if (isMoving)
            {
                move(getNextSpace());
                if (isOnNextSpace())
                {
                    currentSpace = getNextSpace();
                    if (toMove == 0)
                    {
                        isMoving = false;
                        stopSpace();
                    }
                    if (getNextSpace().CompareTag("JunctionSpace"))
                    {
                        isMoving = false;
                        stopSpace();
                    }
                    else if (getNextSpace().CompareTag("StarSpace"))
                    {
                        isMoving = false;
                        stopSpace();
                    }
                    else
                    {
                        toMove--;
                    }
                }
            }

        }
        if (!active && !onEdge)
            moveToEdge();
	}

    private bool rankCompare(Player a)
    {
        //returns true if this player is tied or higher rank compared to another player
        if (stars == a.stars)
        { return coins >= a.coins; }
        else
        { return stars >= a.stars; }

    }

    public void setRank(Player a, Player b, Player c)
    {
        //Lowers rank (thus making them higher ranking) every time the player is tied or higher ranked compared to another player
        rank = 4;
        if (rankCompare(a))
            rank--;
        if (rankCompare(b))
            rank--;
        if (rankCompare(c))
            rank--;
    }

    public void move(GameObject s)
    {
        if (s.transform.position.x > transform.position.x)
            transform.Translate(Vector3.right * Time.deltaTime);
        if (s.transform.position.x < transform.position.x)
            transform.Translate(Vector3.left * Time.deltaTime);
        if (s.transform.position.z > transform.position.x)
            transform.Translate(Vector3.forward * Time.deltaTime);
        if (s.transform.position.z < transform.position.x)
            transform.Translate(Vector3.back * Time.deltaTime);
    }

    public bool isOnNextSpace()
    {
        GameObject n = getNextSpace();
        if (transform.position.x == n.transform.position.x && transform.position.y == n.transform.position.y)
            return true;
        return false;
    }

    public void moveToEdge()
    {
        //snaps the player above its current space when its not its turn, snaps it back to center when it is its turn
        if (!onEdge)
        {
            transform.position = new Vector3(currentSpace.transform.position.x, 0, currentSpace.transform.position.z+30);
            onEdge = true;
        }
        else
        {
            transform.position = new Vector3(currentSpace.transform.position.x,0,currentSpace.transform.position.z);
            onEdge = false;
        }
    }

    public void stopSpace()
    {
        if (currentSpace.CompareTag("BlueSpace"))
        {
        }
        if (currentSpace.CompareTag("RedSpace"))
        {
        }
        if (currentSpace.CompareTag("StarSpace"))
        {
        }
        if (currentSpace.CompareTag("StartSpace"))
        {
        }
        if (currentSpace.CompareTag("JunctionSpace"))
        {
        }
    }
    public GameObject getNextSpace()
    {
        return currentSpace.GetComponent<getNextSpace>().nextSpace();
    }


}
