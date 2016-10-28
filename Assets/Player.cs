using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    
    public string playerName;
    public int stars = 0,coins,highestCoins=0,mgWon=0,blueCount=0,redCount=0,rank=1,turnOrder,toMove;
    public bool active=false,rolling=false,isMoving=false,onEdge=false,onAltPath=false;
    private GameObject currentSpace,nextSpace;
    //Should coins just start at 10? What does the starting point count as spacewise?
    

	// Use this for initialization
	void Start () {
        currentSpace = GameObject.Find("StartSpace");
        nextSpace = GameObject.Find("Space 0");
    }
    	
	// Update is called once per frame  
	void Update () {
        //testing movement - hit up twice:
        if (Input.GetKey("up"))
        {
            active = true;
        }
        if (active)
        {
            if (!isMoving && toMove==0) { rolling = true; }
            if (onEdge)
                moveToEdge();
            if (rolling)
            {
                if (Input.GetKey("up"))
                {
                    toMove=Random.Range(1,6);
                    isMoving = true;
                    rolling = false;
                }
            }
            if (isMoving)
            {
                //move(getNextSpace(nextSpace));
                move(nextSpace);
                if (isOnNextSpace())
                {
                    if (nextSpace.CompareTag("JunctionSpace"))
                    {
                        isMoving = false;
                        stopSpace();
                    }
                    else if (nextSpace.CompareTag("StarSpace"))
                    {
                        isMoving = false;
                        stopSpace();
                    }
                    else
                    {
                        toMove--;
                    }
                    //currentSpace = getNextSpace();
                    currentSpace = nextSpace;
                    nextSpace = getNextSpace();
                    if (toMove == 0)
                    {
                        isMoving = false;
                        active = false;
                        stopSpace();
                    }
                }
            }
            if (!isMoving)
            {
                if (currentSpace.CompareTag("JunctionSpace"))
                {
                    if (Input.GetKey("left")||Input.GetKey("right"))
                    { onAltPath = !onAltPath; }
                    if (Input.GetKey("up"))
                    {
                        if (onAltPath) { nextSpace = currentSpace.GetComponent<getJunction>().getSecondarySpace(); }
                        else { nextSpace = currentSpace.GetComponent<getJunction>().getPrimarySpace(); }
                        isMoving = true;
                    }
                }
                if (currentSpace.CompareTag("StarSpace"))
                {
                    isMoving = true;
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
            transform.Translate(Vector3.right*5);
        if (s.transform.position.x < transform.position.x)
            transform.Translate(Vector3.left*5);
        if (s.transform.position.z > transform.position.z)
            transform.Translate(Vector3.forward*5);
        if (s.transform.position.z < transform.position.z)
            transform.Translate(Vector3.back*5);
    }

    public bool isOnNextSpace()
    {
        return (transform.position.x == nextSpace.transform.position.x && transform.position.z == nextSpace.transform.position.z);
    }

    public void moveToEdge()
    {
        //snaps the player above its current space when its not its turn, snaps it back to center when it is its turn
        if (!onEdge)
        {
            transform.position = new Vector3(currentSpace.transform.position.x, 0, currentSpace.transform.position.z+15);
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
        if (!currentSpace.CompareTag("JunctionSpace"))
            return currentSpace.GetComponent<getNextSpace>().nextSpace();
        else
            return currentSpace.GetComponent<getJunction>().getPrimarySpace();
    }


}
