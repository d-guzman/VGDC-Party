using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    
    private string playerName;
    private int stars = 0,coins,highestCoins=0,mgWon=0,rank=1,turnOrder = -1,toMove,state=0,playersOnSpace=0;
    private int initiative = 0;

    private bool onEdge=false,onAltPath=false,hasInitiative=false;
    private int spaceType;
    public float playerSpeed;
    private Vector3 destination;

    private GameObject currentSpace,nextSpace;
    private GameObject[] players;
    //For state: 0=not their turn, 1=their turn, rolling, 2=moving,3=on junction, 4=on star 5=roll for initiative 6 = turn over

	// Use this for initialization
	void Start () {
        currentSpace = GameObject.Find("StartSpace");
        nextSpace = GameObject.Find("Space 0");
        coins = 10;
        highestCoins = 10;
        state = 0;
        moveToEdge(playersOnSpace);
        spaceType = 0;
        destination = transform.position;
    }
    	
	// Update is called once per frame  
	void Update () {
        //testing movement - hit up twice:
        if (Input.GetKeyDown("down"))
        {
            state=1;
        }
        if(state == 5)
        {
            bool validRoll = false;
            if (Input.GetKeyDown("up"))
            {
                while (!validRoll)
                {
                    bool duplicateRoll = false;
                    int tempInitative = Random.Range(1, 6);
                    for(int i = 0; i < players.Length; i++)
                    {
                        if(tempInitative == players[i].GetComponent<Player>().getInitativeNum())
                        {
                            duplicateRoll = true;
                        }
                    }
                    if (!duplicateRoll)
                    {
                        initiative = tempInitative;
                        validRoll = true;
                        setInitiative(true);
                    }
                }


                setPlayerState(0);
            }
            
        }
        if (state != 0)
        {
            if (onEdge)
                moveToEdge(playersOnSpace);
            if (state==1)
            {
                if (Input.GetKeyDown("up"))
                {
                    toMove = Random.Range(1, 6);
                    state++;
                    destination = nextSpace.transform.position;

                }
            }
            if (state==2)
            {
                //move(getNextSpace(nextSpace));
                //move(nextSpace);
                moveToPoint(destination);
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
                    destination = nextSpace.transform.position;

                    if (toMove == 0)
                    {
                        state = 6;
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
    /*  obsolete, use moveToPoint(Vector3 target) instead
    public void move(GameObject s)
    {
        
        if (s.transform.position.x > transform.position.x)
            transform.Translate(Vector3.right*playerSpeed);
        if (s.transform.position.x < transform.position.x)
            transform.Translate(Vector3.left*playerSpeed);
        if (s.transform.position.z > transform.position.z)
            transform.Translate(Vector3.forward*playerSpeed);
        if (s.transform.position.z < transform.position.z)
            transform.Translate(Vector3.back*playerSpeed);
    }
    */
    public void moveToPoint(Vector3 target)
    {
        Vector3 directionVector = (target - transform.position);
        directionVector.Normalize();
        if(Vector3.Distance(target, transform.position) < playerSpeed)
        {
            transform.position = target;
        } else
        {
            transform.Translate(directionVector * playerSpeed);
        }
        

    }
    public bool isOnNextSpace()
    {
        return (transform.position.x == nextSpace.transform.position.x && transform.position.z == nextSpace.transform.position.z);
    }
    public void moveToCorner()
    {
        int playersOnCurrentSpace = 0;
        for(int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<Player>().onSpace(currentSpace))
            {
                playersOnCurrentSpace++;
            }
        }
        destination = currentSpace.transform.position;
        int spaceRadius = 1;
        //remember this player is on the space as well
        if(playersOnCurrentSpace == 0)
        {
            destination += (Vector3.left + Vector3.forward) * spaceRadius;
        } else if(playersOnCurrentSpace == 1)
        {
            destination += (Vector3.right + Vector3.forward) * spaceRadius;
        } else if(playersOnCurrentSpace == 2)
        {
            destination += (Vector3.left + Vector3.back) * spaceRadius;
        } else
        {
            destination += (Vector3.right + Vector3.back) * spaceRadius;
        }
    }
    public void moveToCenter()
    {
        destination = currentSpace.transform.position;
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
            spaceType = 0;
        }
        if (currentSpace.CompareTag("RedSpace"))
        {
            if (coins >= 3)
                coins -= 3;
            else
                coins = 0;
            spaceType = 1;
        }
        if (currentSpace.CompareTag("StarSpace"))
        {
            //better to do this in the update section (like with junctions)
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
    //get methods and set/changing methods
    public int getCoins() { return coins; }
    public int getHighestCoins() { return highestCoins; }
    public int getStars() { return stars; }
    public int getRoll() { return toMove; }
    public int getMinigamesWon() { return mgWon; }
   
    public int getRank() { return rank; }
    public int getTurnOrder() { return turnOrder; }
    public int getToMove() { return toMove; }
    public int getState() { return state; }
    public int getPlayersOnSpace() { return playersOnSpace; }
    public bool getInitiative() {
        return hasInitiative;
    }
    public void wonMiniGame() { coins += 10; }
    public void addStar() { stars++; }
    public void setRank(int i) { rank = i; }
    public void setInitiative(bool b) { hasInitiative = b; }
    public void setPlayersOnSpace(int i) { playersOnSpace = i; }
    public void setTurnOrder(int i) { turnOrder = i; }
    public bool onSpace(GameObject space)
    {
        return currentSpace.GetInstanceID() == space.GetInstanceID();
    }
    public int getInitativeNum()
    {
        return initiative;
    }
    public void setPlayerState(int x)
    {
        //this code runs once whenever a state is changed
        //use this for initializing values for a player state
        //so you don't need to keep recalculating certain things or repeating actions
        //USE THIS CONSISTENTLY PLEASE

        state = x;
        if(state == 0)
        {

        } else if(state == 1)
        {

        } else if(state == 2)
        {

        } else if(state == 3)
        {

        } else if(state == 4)
        {

        } else if(state == 5)
        {

        } else if(state == 6)
        {

        }
    }
    public void setListOfPlayers(GameObject[] x)
    {
        players = x;
    }
    public int getSpaceType()
    {
        return spaceType;
    }
}
