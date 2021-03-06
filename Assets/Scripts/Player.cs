﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Player : MonoBehaviour {
    
    private string playerName;
    private int stars ,coins,mgWon=0,rank=0,turnOrder = -1,toMove,state=0,playersOnSpace=0;
    private int initiative = 0;

    private bool onEdge=false,onAltPath=false,hasInitiative=false;
    private int spaceType;
    public float playerSpeed;
    private Vector3 destination;
    public int playerNum;
    private GameObject currentSpace,nextSpace;
    private GameObject[] players;
    private GameData gameData;
    private DiceScript dice;
    private GameObject starPrompt;
    private GameObject[] starUI;
    private GameObject selectionArrow;
    public GameObject risingText;

    private int starSelection;
    private Vector3 heightOffset;
    public Text moveCounter;
    private GenericRotateToDirection rotationScript;

    //For state: 0=not their turn, 1=their turn, rolling, 2=moving,3=on junction, 4=on star 5=roll for initiative 6 = turn over
    private const int NOTTURN = 0;
    private const int ONTURN = 1;
    private const int MOVING = 2;
    private const int ONJUNCTION = 3;
    private const int ONSTAR = 4;
    private const int GETINITATIVE = 5;
    private const int TURNOVER = 6;
    private const int GAME_OVER = 7;
    private int score; //used to determine ranking
                       // Use this for initialization
    private bool hasRolled;
    private float afterRollDelay;
    private GameObject junctionArrow;
    private bool ignoreNextJunction;
    private string aButton, bButton;
    private string baseControllerName;
    private bool stopRepeat;
    private Quaternion baseRotation;
    private AudioSource audioSource;
    public AudioClip onSpaceSound;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentSpace = GameObject.Find("StartSpace");
        nextSpace = GameObject.Find("Space 0");
        junctionArrow = GameObject.Find("JunctionArrow");

    }
    void Start () {
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        currentSpace = GameObject.Find(gameData.getCurrentSpace(playerNum));
        nextSpace = GameObject.Find(gameData.getNextSpace(playerNum));
        coins = gameData.getCoins(playerNum);
        stars = gameData.getStars(playerNum);
        transform.position = gameData.getPos(playerNum);
        turnOrder = gameData.getTurnOrder(playerNum);
        players = GameObject.FindGameObjectsWithTag("BoardPlayer");
        dice = GameObject.Find("Dice").GetComponent<DiceScript>();
        starPrompt = GameObject.Find("StarPrompt");
        starUI = new GameObject[5];
        for(int i = 0; i < starUI.Length; i++)
        {
            starUI[i] = starPrompt.transform.GetChild(i).gameObject;
        }
        selectionArrow = GameObject.Find("SelectionArrow");
        setPlayerState(0);
        heightOffset = Vector3.up * 6.5f;
        moveToCorner();
        transform.position = destination;

        score = 0;
        spaceType = 2;
        forceDistributePlayers(currentSpace);
        hasRolled = false;
        afterRollDelay = 0;
        junctionArrow.SetActive(false);
        moveCounter.enabled = false;
        rotationScript = GetComponentInChildren<GenericRotateToDirection>();
        starSelection = 1; //1 is yes, -1 is no
        ignoreNextJunction = false;
        baseControllerName = "P" + (playerNum + 1);
        aButton = baseControllerName + "_Fire1";
        bButton = baseControllerName + "_Fire2";
        stopRepeat = false;
        baseRotation = transform.rotation;
    }
    public void setPlayerState(int x)
    {
        //this code runs once whenever a state is changed
        //use this for initializing values for a player state
        //so you don't need to keep recalculating certain things or repeating actions
        //USE THIS CONSISTENTLY PLEASE

        state = x;
        if (state == NOTTURN)
        {
            

        }
        else if (state == ONTURN)
        {
            moveToCenter();
            afterRollDelay = 0.5f;
            hasRolled = false;
            dice.rollDice();
            dice.revealDice(currentSpace.transform.position);
        }
        else if (state == MOVING)
        {
            dice.hideDice();
        }
        else if (state == ONJUNCTION)
        {
            if (!junctionArrow.activeSelf)
            {
                junctionArrow.SetActive(true);
            }
        }
        else if (state == ONSTAR)
        {

        }
        else if (state == GETINITATIVE)
        {
            dice.hideDice();
            dice.revealDice(destination);
            dice.rollDice();
            hasRolled = false;
            afterRollDelay = 0.5f;
        }
        else if (state == TURNOVER)
        {


        }
        /*
     NOTTURN = 0;
     ONTURN = 1;
     MOVING = 2;
     ONJUNCTION = 3;
     ONSTAR = 4;
     GETINITATIVE = 5;
     TURNOVER = 6;
     */
    }
    // Update is called once per frame  
    void Update () {
        //testing movement - hit up twice:
        if(state == GAME_OVER)
        {
            dice.hideDice();
        }
        if (state == GETINITATIVE)
        {
            if (!dice.isRevealed())
            {
                dice.revealDice(transform.position);
            }

            
            if ((Input.GetKeyDown("up") || Input.GetButtonDown(aButton)) && !hasInitiative && !hasRolled)
            {
                bool validRoll = false;
                while (!validRoll)
                {
                    bool duplicateRoll = false;
                    int tempInitiative = Random.Range(1, 7);
                    initiative = 0;
                    for(int i = 0; i < players.Length; i++)
                    {
                        
                        if(tempInitiative == players[i].GetComponent<Player>().getInitativeNum())
                        {
                            duplicateRoll = true;
                        }
                        
                    }

                    if (!duplicateRoll)
                    {
                        initiative = tempInitiative;
                        validRoll = true;
                        hasRolled = true;
                    }
                }
                dice.stopDice(initiative);
            }
            if(hasRolled)
            {
                if(afterRollDelay > 0)
                {
                    afterRollDelay -= Time.deltaTime;
                } else
                {
                    setInitiative(true);
                    setPlayerState(NOTTURN);
                    dice.hideDice();
                    
                }
                
            }
        }
        moveToPoint(destination);//player will move to any destination specified
                                 //you can just change the destination once and the player will reach it
        if (state != NOTTURN)
        {

              

            if (state==ONTURN)
            {
                
                if ((Input.GetKeyDown("up") || Input.GetButtonDown(aButton)) && !hasRolled)
                {
                    toMove = Random.Range(1, 7);
   
                    dice.stopDice(toMove);
                    hasRolled = true;
                }

                if (hasRolled)
                {
                    if (afterRollDelay > 0)
                    {
                        afterRollDelay -= Time.deltaTime;
                    }
                    else
                    {
                        setPlayerState(MOVING);
                        destination = nextSpace.transform.position + heightOffset;
                    }
                }
                
                 
                      
                    
                
            }
            if (state==MOVING)
            {
                rotationScript.setTargetDirection(nextSpace.transform.position-currentSpace.transform.position);
                moveCounter.enabled = true;
                moveCounter.text = ""+toMove;
                //move(getNextSpace(nextSpace));
                //move(nextSpace);
                if (isOnNextSpace())
                {
                    
                    currentSpace = nextSpace;
                    nextSpace = getNextSpace();
                    destination = nextSpace.transform.position + heightOffset;
                    if (currentSpace.CompareTag("JunctionSpace"))
                    {
                        setPlayerState(ONJUNCTION);
                        destination = currentSpace.transform.position + heightOffset;

                    }
                    else if (currentSpace.CompareTag("StarSpace"))
                    {
                       
                        ignoreNextJunction = false;
                      
                        setPlayerState(ONSTAR);
                        destination = currentSpace.transform.position + heightOffset;
                    }
                    else
                    {
                       
                        ignoreNextJunction = false;
                        audioSource.PlayOneShot(onSpaceSound);
                        toMove--;
                    }
                    //currentSpace = getNextSpace();
                    

                    if (toMove <= 0)
                    {
                        moveCounter.enabled = false;
                        setPlayerState(TURNOVER);   //signals gamecontroller that this turn is over, gamecontroller will set this back to 0
                        stopSpace();
                        destination = currentSpace.transform.position + heightOffset;
                        rotationScript.setTargetDirection((destination - transform.position));
                    }
                }
                
            }
            if (state == ONJUNCTION)
            {
                if (!ignoreNextJunction)
                {
                    if (Input.GetKeyDown("left") || Input.GetKeyDown("right") || 
                        (Input.GetAxisRaw(baseControllerName + "_DPad_X") == 1 || 
                        Input.GetAxisRaw(baseControllerName + "_DPad_X") == -1))
                    {
                        if (!stopRepeat)
                        { onAltPath = !onAltPath; }
                        stopRepeat = true;
                    }
                    if(Input.GetAxisRaw(baseControllerName + "_DPad_X") == 0)
                    {
                        stopRepeat = false;
                    }

                    if (Input.GetKeyDown("up") || Input.GetButtonDown(aButton))
                    {
                        ignoreNextJunction = true;
                        if (onAltPath)
                        {
                            nextSpace = currentSpace.GetComponent<getJunction>().getSecondarySpace();
                        }
                        else
                        {
                            nextSpace = currentSpace.GetComponent<getJunction>().getPrimarySpace();
                        }
                        state = MOVING;
                        destination = nextSpace.transform.position + heightOffset;
                        junctionArrow.SetActive(false);
                    }

                    if (onAltPath)
                    {
                        junctionArrow.transform.position = currentSpace.transform.position;

                        junctionArrow.transform.LookAt(currentSpace.GetComponent<getJunction>().getSecondarySpace().transform.position);
                        junctionArrow.transform.position = currentSpace.transform.position;
                        junctionArrow.transform.position += junctionArrow.transform.forward * 30 + Vector3.up * 5;
                        rotationScript.setTargetDirection(currentSpace.GetComponent<getJunction>().getSecondarySpace().transform.position - currentSpace.transform.position);

                    }
                    else
                    {
                        junctionArrow.transform.position = currentSpace.transform.position;

                        junctionArrow.transform.LookAt(currentSpace.GetComponent<getJunction>().getPrimarySpace().transform.position);
                        junctionArrow.transform.position = currentSpace.transform.position;
                        junctionArrow.transform.position += junctionArrow.transform.forward * 30 + Vector3.up * 5;
                        rotationScript.setTargetDirection(currentSpace.GetComponent<getJunction>().getPrimarySpace().transform.position - currentSpace.transform.position);

                    }

                }
                else
                {
                    junctionArrow.SetActive(false);
                    nextSpace = currentSpace.GetComponent<getJunction>().getPrimarySpace();
                    state = MOVING;
                    destination = nextSpace.transform.position + heightOffset;
                }

            }
            if (state == ONSTAR)
            {

                if (getCoins() >= 20)
                {
                    starUI[0].GetComponent<Text>().text = "Would you like to buy a STAR?";
                    starUI[1].SetActive(true);
                    starUI[2].SetActive(true);
                    starUI[3].SetActive(false);
                    starUI[4].SetActive(true);
                } else
                {
                    starUI[0].GetComponent<Text>().text = "You are too poor to buy a STAR";
                    starUI[1].SetActive(false);
                    starUI[2].SetActive(false);
                    starUI[3].SetActive(true);
                    starUI[4].SetActive(true);
                }
                if (!starPrompt.GetComponent<UIRevealer>().revealed)
                {
                    starPrompt.GetComponent<UIRevealer>().revealUI();
                }
                if (getCoins() >= 20)
                {
                    if ((Input.GetKeyDown("left") || Input.GetAxisRaw(baseControllerName + "_DPad_X") == 1) 
                        && starSelection == -1)
                    {
                        starSelection = 1;
                    }
                    else if ((Input.GetKeyDown("right") || Input.GetAxisRaw(baseControllerName + "_DPad_X") == 1) 
                        && starSelection == 1)
                    {
                        starSelection = -1;
                    }
                    if (starSelection == 1)
                    {
                        starUI[4].transform.position = starUI[1].transform.position + Vector3.left 
                            * starUI[1].GetComponent<Text>().preferredWidth;
                    }
                    else
                    {
                        starUI[4].transform.position = starUI[2].transform.position + Vector3.left 
                            * starUI[2].GetComponent<Text>().preferredWidth;
                    }
                    if (Input.GetKeyDown("up") || Input.GetButtonDown(aButton))
                    {
                        if (starSelection == 1)
                        {
                            stars += 1;
                            createFloatingText("+1");
                            coins -= 20;
                            GameObject.Find("BoardSpaces").GetComponent<RandomizeStarSpace>().moveStarSpace();
                        }
                        destination = nextSpace.transform.position + heightOffset;
                        starPrompt.GetComponent<UIRevealer>().hideUI();
                        state = MOVING;
                    }
                    if (Input.GetButtonDown(bButton))
                    {
                        destination = nextSpace.transform.position + heightOffset;
                        starPrompt.GetComponent<UIRevealer>().hideUI();
                        state = MOVING;
                    }
                } else
                {
                    starUI[4].transform.position = starUI[3].transform.position + Vector3.left 
                        * starUI[3].GetComponent<Text>().preferredWidth;
                    if (Input.GetKeyDown("up") || Input.GetButtonDown(aButton) || Input.GetButtonDown(bButton))
                    {
                        starPrompt.GetComponent<UIRevealer>().hideUI();
                        state = MOVING;
                        destination = nextSpace.transform.position + heightOffset;

                    }

                }

            }
        }
        else
        {
            //this code runs when it's not a player's turn
            rotationScript.setTargetDirection(Vector3.forward);
        }
        
	}    
   
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
    public void forceDistributePlayers(GameObject space)
    {
        //move to corner only works if players reach a location sequentially
        //aka everyone goes to the "4 player" position when the game starts
        //this will distribute anyone on a given spot
        int spaceRadius = 10;

        Vector3[] offset = new Vector3[4];
        offset[0] = (Vector3.left + Vector3.forward) * spaceRadius;
        offset[1] = (Vector3.right + Vector3.forward) * spaceRadius;
        offset[2] = (Vector3.left + Vector3.back) * spaceRadius;
        offset[3] = (Vector3.right + Vector3.back) * spaceRadius;
        List<GameObject> playerList = new List<GameObject>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<Player>().onSpace(currentSpace))
            {
                playerList.Add(players[i]);
            }
        }
        Vector3 tempDestination = currentSpace.transform.position + heightOffset;
        for(int i = 0; i < playerList.Count; i++)
        {
            playerList[i].GetComponent<Player>().setDestination(tempDestination + offset[i]);
        }
        
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

        Vector3 tempDestination = currentSpace.transform.position + heightOffset;
        int spaceRadius = 10;
        //remember this player is on the space as well
        if(playersOnCurrentSpace == 1)
        {
            tempDestination += (Vector3.left + Vector3.forward) * spaceRadius;
        } else if(playersOnCurrentSpace == 2)
        {
            tempDestination += (Vector3.right + Vector3.forward) * spaceRadius;
        } else if(playersOnCurrentSpace == 3)
        {
            tempDestination += (Vector3.left + Vector3.back) * spaceRadius;
        } else
        {
            tempDestination += (Vector3.right + Vector3.back) * spaceRadius;
        }
        destination = tempDestination;
    }
    public void moveToCenter()
    {
        destination = currentSpace.transform.position + heightOffset;
    }
    
    public void stopSpace()
    {
        if (currentSpace.CompareTag("BlueSpace"))
        {
            coins += 3;
            spaceType = 0;
            createFloatingText("+3");

        }
        if (currentSpace.CompareTag("RedSpace"))
        {
            createFloatingText("-3");
            if (coins >= 3)
                coins -= 3;
            else
                coins = 0;
            spaceType = 1;
        }
       
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
    public int getStars() { return stars; }
    public int getRoll() { return toMove; }
    public int getMinigamesWon() { return mgWon; }
   

    public int getTurnOrder() { return turnOrder; }
    public int getToMove() { return toMove; }
    public int getState() { return state; }
    public int getPlayersOnSpace() { return playersOnSpace; }
    public bool getInitiative() {
        return hasInitiative;
    }
    public void wonMiniGame() { coins += 10; }
    public void addStar() { stars++; }
    public void setInitiative(bool b) { hasInitiative = b; }
    public void setPlayersOnSpace(int i) { playersOnSpace = i; }
    public void setTurnOrder(int i) { turnOrder = i; }
    public void setDestination(Vector3 dest)
    {
        destination = dest;
    }
    public bool onSpace(GameObject space)
    {
        return currentSpace.GetInstanceID() == space.GetInstanceID();
    }
    public int getInitativeNum()
    {
        return initiative;
    }

    public int getRank() {
        return rank;
    }

    public void setRank(int x)
    {
        rank = x;
    }

   
    public void setListOfPlayers(GameObject[] x)
    {
        players = x;
    }
    public int getSpaceType()
    {
        return spaceType;
    }
    public void setSpaceType(int x)
    {
        spaceType = x;
    }
    public int getScore()
    {
        //whoever has more stars WILL win, but in the case of a star tie, whoever has more coins will win
        //coin count must remain under 1000 to be valid (this shouldn't happen, right?)
        return stars * 1000 + coins;
    }
    public int getPlayerNum()
    {
        return playerNum;
    }
    private void createFloatingText(string msg)
    {
        GameObject obj;
        obj = Instantiate(risingText);
        obj.GetComponent<FloatingText>().setText(msg);
        obj.GetComponent<FloatingText>().setPlayer(this.gameObject);
    }
    public GameObject getCurrentSpace()
    {
        return currentSpace;
    }
    public GameObject returnNextSpace()
    {
        return nextSpace;
    }
}
