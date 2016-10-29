using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    
    public string playerName;
    public int stars = 0,coins,highestCoins=0,mgWon=0,blueCount=0,redCount=0,rank=1,turnOrder,toMove,state=0,playersOnSpace=0;
    public bool onEdge=false,onAltPath=false,hasInitiative=false;
    private GameObject currentSpace,nextSpace;
    
    //For state: 0=not their turn, 1=their turn, rolling, 2=moving,3=on junction, 4=on star

	// Use this for initialization
	void Start () {
        currentSpace = GameObject.Find("StartSpace");
        nextSpace = GameObject.Find("Space 0");
        coins = 10;
        highestCoins = 10;
        state = 0;
        moveToEdge(playersOnSpace);
    }
    	
	// Update is called once per frame  
	void Update () {
        //testing movement - hit up twice:
        if (Input.GetKeyDown("down"))
        {
            state=1;
        }
        if (state > 0)
        {
            if (onEdge)
                moveToEdge(playersOnSpace);
            if (state==1)
            {
                if (Input.GetKeyDown("up"))
                {
                    toMove = Random.Range(1, 6);
                    state++;
                }
            }
            if (state==2)
            {
                //move(getNextSpace(nextSpace));
                move(nextSpace);
                if (isOnNextSpace())
                {
                    if (nextSpace.CompareTag("JunctionSpace"))
                    {
                        state = 3;
                        stopSpace();
                    }
                    else if (nextSpace.CompareTag("StarSpace"))
                    {
                        state = 4;
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
                        state = 0;
                        stopSpace();
                    }
                }
            }
            if (state == 3)
            {
                if (Input.GetKeyDown("left") || Input.GetKeyDown("right"))
                { onAltPath = !onAltPath; }
                if (Input.GetKeyDown("up"))
                {
                    if (onAltPath) { nextSpace = currentSpace.GetComponent<getJunction>().getSecondarySpace(); }
                    else { nextSpace = currentSpace.GetComponent<getJunction>().getPrimarySpace(); }
                    state = 2;
                }
            }
            if (state == 4)
            {
                state = 2;
            }
        }
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
    public void setTurnOrder(int i) { turnOrder = i; }
    public bool isOnNextSpace()
    {
        return (transform.position.x == nextSpace.transform.position.x && transform.position.z == nextSpace.transform.position.z);
    }

    public void moveToEdge(int i)
    {
        //snaps the player above its current space when its not its turn, snaps it back to center when it is its turn
        if (!onEdge)
        {
            //Moves the player to a different corner depending on its playersOnSpace value. Each player on the space increases playersOnSpace for the next player to land by 1
            if (i == 0)
            {
                transform.position = new Vector3(currentSpace.transform.position.x - 15, 0, currentSpace.transform.position.z + 15);
                onEdge = true;
            }
            else if (i == 1)
            {
                transform.position = new Vector3(currentSpace.transform.position.x + 15, 0, currentSpace.transform.position.z + 15);
                onEdge = true;
            }
            else if (i == 2)
            {
                transform.position = new Vector3(currentSpace.transform.position.x - 15, 0, currentSpace.transform.position.z - 15);
                onEdge = true;
            }
            else if (i == 3)
            {
                transform.position = new Vector3(currentSpace.transform.position.x + 15, 0, currentSpace.transform.position.z - 15);
                onEdge = true;
            }
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
            coins += 3;
            blueCount++;
            moveToEdge(playersOnSpace);
        }
        if (currentSpace.CompareTag("RedSpace"))
        {
            if (coins >= 3)
                coins -= 3;
            else
                coins = 0;
            redCount++;
            moveToEdge(playersOnSpace);
        }
        if (currentSpace.CompareTag("StarSpace"))
        {
        }
        //setRank(other players go here);
        if (coins > highestCoins)
            highestCoins = coins;
    }
    public GameObject getNextSpace()
    {
        if (!currentSpace.CompareTag("JunctionSpace"))
            return currentSpace.GetComponent<getNextSpace>().nextSpace();
        else
            return currentSpace.GetComponent<getJunction>().getPrimarySpace();
    }
    public int getCoins() { return coins; }
    public int getStars() { return stars; }
    public int getRoll() { return toMove; }
    public int getPlayersOnSpace() { return playersOnSpace; }
    public void wonMiniGame() { coins += 10; }
    public void setPlayersOnSpace(int i) { playersOnSpace = i; }

}
