﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameController : MonoBehaviour {




    // Use this for initialization
    GameObject cam;
    Player currentPlayer;
    Player[] playerList;
    TurnCounter turnCounter;
    GameData gameData;
    UIRevealer[] minigameUI;
    UIRevealer[] playerTabs;
    UIRevealer tieBreakUI;
    UIRevealer turnCounterUI;
    UIRevealer lowerScreenUI;
    UIRevealer blackPanel;
    UIRevealer initiativeUI;
    UIRevealer initiativePromptUI;
    LowerScreenTextScript lowerScreenText;
    Image lowerScreenBackground;
    UIRevealer noMoreTurnsUI;
    int gameState;

    private const int GAME_BOARD = 1;
    private const int NO_MORE_TURNS = 4;
    private const int TIE_BREAKER = 3;
    private const int GAME_OVER = 2;

    int boardState;
    private const int PRE_GAME = 0;
    private const int GET_INITIATIVE = 1;
    private const int NEW_TURN = 2;
    private const int PLAYERS_TURN = 3;
    private const int DECIDE_MINIGAME = 4;
    private const int BACK_FROM_MINIGAME = 5;
    private int playerTurn;

    private int minigameType;

    public string[] minigamesFFA;     //scene names go in here
    public string[] minigames2v2;   //scene names go in here
    public string[] minigames1v3;   //scene names go in here
    
    private string[][] minigameList;
    private float beforePlayerTurnTimer;
    private float afterPlayerTurnTimer;
    private float genericDelay;
    public ReadyGate initiativeReadyGate;
    public ReadyGate tieBreakReadyGate;
    private dice2d flatDice;
    List<Player> winnerList;
    int[] validNumbers;
    bool tied;
    AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip jumpSound;
    public AudioClip getStarSound;
    void Start()
    {
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        loadGameObjects();
        
        setCameraPreset(1);
        playerTurn = 0;
        boardState =  gameData.getBoardState();
        gameState = gameData.getGameState();
        setBoardState(boardState);

        setPlayerRanks();
        
        
        beforePlayerTurnTimer = 0f;
        afterPlayerTurnTimer = 0f;
        genericDelay = 2f;
        tied = false;
    }
    public void loadGameObjects()
    {
        audioSource = GetComponent<AudioSource>();
        playerList = new Player[4];
        playerList[0] = GameObject.Find("boardPlayer1").GetComponent<Player>();
        playerList[1] = GameObject.Find("boardPlayer2").GetComponent<Player>();
        playerList[2] = GameObject.Find("boardPlayer3").GetComponent<Player>();
        playerList[3] = GameObject.Find("boardPlayer4").GetComponent<Player>();
        GameObject[] temp = new GameObject[4];
        temp[0] = GameObject.Find("boardPlayer1");
        temp[1] = GameObject.Find("boardPlayer2");
        temp[2] = GameObject.Find("boardPlayer3");
        temp[3] = GameObject.Find("boardPlayer4");
        for (int i = 0; i < playerList.Length; i++)
        {
            playerList[i].setListOfPlayers(temp);
        }
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        turnCounter = GameObject.Find("TurnCounter").GetComponent<TurnCounter>();
        turnCounter.decrementTurnCount(); //decrement at the beginning of each turn
        //initial value with be 1 greater to counter first turn decrement
        noMoreTurnsUI = GameObject.Find("NoMoreTurnsUI").GetComponent<UIRevealer>();
        GameObject lowerScreen = GameObject.Find("LowerScreen");
        lowerScreenUI = lowerScreen.GetComponent<UIRevealer>();
        lowerScreenText = GameObject.Find("LowerScreenText").GetComponent<LowerScreenTextScript>();
        lowerScreenBackground = lowerScreen.GetComponent<Image>();
        initiativeUI = GameObject.Find("InitiativeUI").GetComponent<UIRevealer>();
        initiativePromptUI = GameObject.Find("InitiativePromptBack").GetComponent<UIRevealer>();
        playerTabs = new UIRevealer[4];
        playerTabs[0] = GameObject.Find("Player1Tab").GetComponent<UIRevealer>();
        playerTabs[1] = GameObject.Find("Player2Tab").GetComponent<UIRevealer>();
        playerTabs[2] = GameObject.Find("Player3Tab").GetComponent<UIRevealer>();
        playerTabs[3] = GameObject.Find("Player4Tab").GetComponent<UIRevealer>();
        flatDice = GameObject.Find("DiceBackground").GetComponent<dice2d>();
        tieBreakUI = GameObject.Find("TieBreakUI").GetComponent<UIRevealer>();
        blackPanel = GameObject.Find("BlackPanel").GetComponent<UIRevealer>();

        minigameUI = new UIRevealer[3];
        minigameList = new string[3][] { minigamesFFA, minigames2v2, minigames1v3 };
        GameObject tempObject = GameObject.Find("MinigameUI");
        for (int i = 0; i < minigameUI.Length; i++)
        {
            minigameUI[i] = tempObject.transform.GetChild(i).gameObject.GetComponent<UIRevealer>();
        }
        turnCounterUI = GameObject.Find("TurnUIUpper").GetComponent<UIRevealer>();
    }
    void Update()
    {
        if(turnCounter.getTurnCount() <= 0 && gameState == GAME_BOARD)
        {
            setGameState(NO_MORE_TURNS);
        }

        


        //game flow is under this
       if(gameState == 1)
        {
            if (Input.GetKeyDown("1"))
            {
                playerList[0].setSpaceType(0);
                playerList[1].setSpaceType(0);
                playerList[2].setSpaceType(0);
                playerList[3].setSpaceType(0);
                
                gameState = 1;
                //SceneManager.LoadScene(minigameList[type][rngGame]);     //this is correct implementation, but requires at least 1 of every game type
                loadScene("mini_arena");
            }
            if (Input.GetKeyDown("2"))
            {
                playerList[0].setSpaceType(0);
                playerList[1].setSpaceType(0);
                playerList[2].setSpaceType(0);
                playerList[3].setSpaceType(0);

                gameState = 1;
                //SceneManager.LoadScene(minigameList[type][rngGame]);     //this is correct implementation, but requires at least 1 of every game type
                loadScene("soccerField");
            }
            if (Input.GetKeyDown("3"))
            {
                playerList[0].setSpaceType(0);
                playerList[1].setSpaceType(0);
                playerList[2].setSpaceType(0);
                playerList[3].setSpaceType(0);

                gameState = 1;
                //SceneManager.LoadScene(minigameList[type][rngGame]);     //this is correct implementation, but requires at least 1 of every game type
                loadScene("hand cart");
            }
            //print(boardState);
            if (boardState == PRE_GAME)
            {
                initiativeReadyGate.allowReadying(true);
                if (initiativeReadyGate.allPlayersReady())
                {
                    initiativeUI.hideUI();
                    setBoardState(GET_INITIATIVE);
                    
                }
            }
            else if (boardState == GET_INITIATIVE)
            {
                int rollingPlayer = -1;

                for (int j = 0; j < playerList.Length; j++)
                {
                    if (!playerList[j].getInitiative())
                    {
                        rollingPlayer = j;
                        j = 100000; //break out of loop
                    }

                }

                if (rollingPlayer != -1)
                {
                    followPlayer(playerList[rollingPlayer].gameObject);
                    if (playerList[rollingPlayer].getState() != 5)
                    {
                        playerList[rollingPlayer].setPlayerState(5);
                    }
                    lowerScreenUI.revealUI();
                    lowerScreenText.setText(playerList[rollingPlayer].getPlayerNum() + 4);
                    if(playerList[rollingPlayer].getPlayerNum() == 0)
                    {
                        lowerScreenBackground.color = new Color(1, 0, 0, 0.66f);
                        lowerScreenText.setColor(Color.white);
                    } else if(playerList[rollingPlayer].getPlayerNum() == 1)
                    {
                        lowerScreenBackground.color = new Color(1, 1, 0, 0.66f);
                        lowerScreenText.setColor(Color.black);
                    } else if(playerList[rollingPlayer].getPlayerNum() == 2)
                    {
                        lowerScreenBackground.color = new Color(0, 0, 0, 146f / 255f);
                        lowerScreenText.setColor(Color.white);
                    } else if(playerList[rollingPlayer].getPlayerNum() == 3)
                    {
                        lowerScreenBackground.color = new Color(0, 0, 1, 0.66f);
                        lowerScreenText.setColor(Color.white);
                    }
                }
                else
                {
                    setTurnOrder();
                    lowerScreenBackground.color = new Color(0, 0, 0, 146f/255f);
                    lowerScreenText.setColor(Color.white);
                    initiativePromptUI.hideUI();
                    setBoardState(NEW_TURN);
                    
                }
            }
            else if (boardState == BACK_FROM_MINIGAME)
            {
                setPlayerRanks();
                if (genericDelay > 0)
                {
                    genericDelay -= Time.deltaTime;
                    turnCounterUI.revealUI();
                }
                else
                {
                    turnCounterUI.hideUI();
                    setBoardState(NEW_TURN);
                }
            }
            else if (boardState == NEW_TURN)
            {
                setBoardState(PLAYERS_TURN);
                resetPlayerSpaceTypes();
                revealPlayerTabs();

            }
            else if (boardState == PLAYERS_TURN)
            {
                if (beforePlayerTurnTimer > 0)
                {
                    if (playerTurn < playerList.Length)
                    {
                        currentPlayer = getCurrentPlayer(playerTurn);
                        lowerScreenUI.revealForTime(1.2f);
                        lowerScreenText.setText(currentPlayer.getPlayerNum());
                    }

                    beforePlayerTurnTimer -= Time.deltaTime; //this is so we can zoom the camera out before moving to the next player
                }
                else
                {

                    if (playerTurn < playerList.Length)
                    {
                        currentPlayer = getCurrentPlayer(playerTurn);
                        followPlayer(currentPlayer.gameObject);
                        if (currentPlayer.getState() == 0)
                        {

                            currentPlayer.setPlayerState(1);


                        }
                        else if (currentPlayer.getState() == 3)
                        {
                            cam.GetComponentInParent<CamBehavior>().setFollowPlayer(false);
                            cam.GetComponentInParent<CamBehavior>().setTargetLocation(currentPlayer.gameObject.transform.position + new Vector3(0, 300, -150));


                        }
                        else if (currentPlayer.getState() == 6)
                        {
                            if(afterPlayerTurnTimer == 1.2f)
                            {
                                setPlayerRanks();
                            }
                            if (afterPlayerTurnTimer > 0)
                            {
                                afterPlayerTurnTimer -= Time.deltaTime;
                            }
                            else
                            {
                                afterPlayerTurnTimer = 1.2f;
                                beforePlayerTurnTimer = 1.2f;
                                playerTurn++;
                                currentPlayer.setPlayerState(0);
                                currentPlayer.moveToCorner();
                                setCameraPreset(1);
                            }



                        }
                    }
                    else
                    {
                        setBoardState(DECIDE_MINIGAME);
                    }
                }


            }
            else if (boardState == DECIDE_MINIGAME)
            {
                bool tempDisable = false;
                if (!tempDisable)
                {
                    for (int i = 0; i < minigameUI.Length; i++)
                    {
                        minigameUI[i].revealUI();
                    }
                    if (genericDelay > 0)
                    {
                        genericDelay -= Time.deltaTime;
                        if (genericDelay < 2.2f)
                        {
                            minigameUI[minigameType].gameObject.GetComponentInChildren<Text>().color = Color.red;
                        }
                        if (genericDelay < 0.7f)
                        {
                            blackPanel.revealUI();
                        }

                    }
                    else
                    {
                        boardState = NEW_TURN;
                        startMinigame(minigameType);
                    }

                }
                else
                {
                    setBoardState(NEW_TURN);
                }
            }
        } else if(gameState == TIE_BREAKER)
        {
            if (tieBreakReadyGate.allPlayersReady())
            {
                tieBreakReadyGate.allowReadying(false);
                genericDelay = 2f;
                int winner = Random.Range(0, validNumbers.Length);
                flatDice.stopDice(validNumbers[winner]+1);
                for(int i = 0; i < playerList.Length; i++)
                {
                    if(playerList[i].getPlayerNum() == validNumbers[winner])
                    {
                        playerList[i].addStar();
                    }
                }
                GameObject.Find("Star" + validNumbers[winner]).GetComponent<UIRevealer>().revealUI();
                tied = false;
            }
            if (!tied)
            {
               setGameState(GAME_OVER);
            }
        } else if(gameState == NO_MORE_TURNS)
        {
            if(genericDelay > 0)
            {
                genericDelay -= Time.deltaTime;
                if(genericDelay < 0.5f)
                {
                    noMoreTurnsUI.hideUI();
                    turnCounterUI.hideUI();
                }
            } else
            {
                if(getWinners().Count > 1)
                {
                    setGameState(TIE_BREAKER);
                } else
                {
                    setGameState(GAME_OVER);
                }
            }
        } else if(gameState == GAME_OVER)
        {
            if(genericDelay > 0)
            {
                genericDelay -= Time.deltaTime;
                if(genericDelay < 0.7f)
                {
                    blackPanel.revealUI();
                }
            } else
            {
                loadScene("end_game");
            }
        }
        if (Input.GetKeyDown("8"))
        {
            setGameState(NO_MORE_TURNS);
        }
    }
    public void setCameraPreset(int x)
    {
        /*
           1: Wide view of entire map
           2: StartSpace
           3: StarSpace

        */
        
        cam.GetComponentInParent<CamBehavior>().setFollowPlayer(false);
        if(x == 1){
            cam.GetComponentInParent<CamBehavior>().setTargetLocation(new Vector3(300, 270, 0));
        } else if(x == 2)
        {
            cam.GetComponentInParent<CamBehavior>().setTargetLocation(new Vector3(500, 80, 0));
        } else if(x == 3)
        {
            GameObject starSpace = GameObject.FindGameObjectWithTag("StarSpace");
            cam.GetComponentInParent<CamBehavior>().setTargetLocation(new Vector3(starSpace.transform.position.x, 80, starSpace.transform.position.z-100));
        }
    }
    public void followPlayer(GameObject player)
    {
        cam.GetComponentInParent<CamBehavior>().setFollowPlayer(true);
        cam.GetComponentInParent<CamBehavior>().setTargetPlayer(player);
    }
    public void runPlayerTurns()
    {

    }
    public void getInitiative()
    {

    }

    public void End()
    {
       
    }
    public int getMinigameType()
    {
        int result = -1;
        int redCount = 0;
        int blueCount = 0;
        for(int i = 0; i < playerList.Length; i++)
        {
            if(playerList[i].getSpaceType() == 0)
            {
                blueCount++;
            } else if(playerList[i].getSpaceType() == 1)
            {
                redCount++;
            }
            else
            {
                print("player on unknown space type");
            }
        }
        if(blueCount == 4 || redCount == 4)
        {
            result = 0;   //FFA
        } else if(blueCount == 2 || redCount == 2)
        {
            result = 1;  //2v2
        } else if(blueCount == 3 || redCount == 3)
        {
            result = 2; //1v3
        } else
        {
            print("Unusual amount of blue and red spaces");
        }
        return result;
    }
    public void loadScene(string name)
    {
        boardState = BACK_FROM_MINIGAME;
        saveData();
        SceneManager.LoadScene(name);
    }
    public void startMinigame(int type)
    {
       
        int rngGame = Random.Range(0, minigameList[type].Length);

        

        //loadScene(minigameList[type][rngGame]);     //this is correct implementation, but requires at least 1 of every game type
        loadScene("mini_arena");

    }
    public void loadGameBoard()
    {
        gameState = GAME_BOARD;
        SceneManager.LoadScene("GameBoard");
        loadGameObjects();
        setCameraPreset(1);

    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        
        SceneManager.LoadScene("start menu");

    }
    public void setGameState(int x)
    {
        //this code runs once whenever a state is changed
        //use this for initializing values for a game state
        //so you don't need to keep recalculating certain things or repeating actions
        //USE THIS CONSISTENTLY PLEASE
        gameState = x;
        if(gameState == GAME_BOARD)
        {
            loadGameBoard();
        } else if(gameState == GAME_OVER)
        {
            genericDelay = 3f;
        } else if(gameState == NO_MORE_TURNS)
        {   
            
            for (int i = 0; i < playerList.Length; i++)
            {
                playerList[i].setPlayerState(7);
            }
            genericDelay = 2.5f;
            for (int i = 0; i < playerTabs.Length; i++)
            {
                playerTabs[i].hideUI();
            }
            lowerScreenUI.hideUI();
            turnCounterUI.revealUI();
            noMoreTurnsUI.revealUI();
        } else if(gameState == TIE_BREAKER)
        {
            
            winnerList = getWinners();
            tied = true;
            validNumbers = new int[winnerList.Count];
            for (int i = 0; i < validNumbers.Length; i++)
            {
                validNumbers[i] = winnerList[i].getPlayerNum();
            }
            flatDice.setValidNumber(validNumbers);

                flatDice.rollDice();
                tieBreakUI.revealUI();
                tieBreakReadyGate.allowReadying(true);
            
        }
    }
    public void setBoardState(int x)
    {
        //this code runs once whenever a state is changed
        //use this for initializing values for a board state
        //so you don't need to keep recalculating certain things or repeating actions
        //USE THIS CONSISTENTLY PLEASE
        boardState = x;
        if(boardState == PRE_GAME)
        {
            setCameraPreset(1);
            initiativeReadyGate.allowReadying(true);
            initiativeUI.revealUI();
            
        } else if(boardState == GET_INITIATIVE)
        {
            setCameraPreset(2);
            initiativePromptUI.revealUI();
        } else if(boardState == NEW_TURN)
        {

        } else if(boardState == PLAYERS_TURN)
        {
            setCameraPreset(1);
            beforePlayerTurnTimer = 1.2f;
            afterPlayerTurnTimer = 1.2f;
            playerTurn = 0;
            //cam.GetComponentInParent<CamBehavior>().setTargetRotation(new Vector3(120, 0, 0));
        } else if(boardState == DECIDE_MINIGAME)
        {
            minigameType = getMinigameType();
            genericDelay = 3.5f;
        } else if(boardState == BACK_FROM_MINIGAME)
        {
            genericDelay = 2f;
        }
    }
    public void setTurnOrder()
    {
        
        for(int i = 0; i < playerList.Length; i++)
        {
            int max = 0;
            int index = 0;
            for(int j = 0; j < playerList.Length; j++)
            {
                if(max < playerList[j].getInitativeNum() && playerList[j].getTurnOrder() == -1)
                {
                    max = playerList[j].getInitativeNum();
                    index = j;
                }
            }
            playerList[index].setTurnOrder(i);
        }
        
    }
    public Player getCurrentPlayer(int x)
    {
        Player result = null;
        for(int i = 0; i < 4; i++)
        {
            if (playerList[i].getTurnOrder() == x)
            {
                result = playerList[i];
            }
        }
        return result;
    }
    public void setPlayerRanks()
    {
        int[] result = new int[4];
        int[][] scores = new int[4][];


        for (int i = 0; i < 4; i++)
        {
            scores[i] = new int[2];
            scores[i][0] = getScore(i);
            scores[i][1] = i;
        }
        
        for (int i = 0; i < 4; i++)
        {
            int max = -1;
            int index = 0;
            for (int j = i; j < 4; j++)
            {
                if (Mathf.Max(max, scores[j][0]) > max)
                {
                    max = scores[j][0];
                    index = j;

                }
            }
            int[] thing = scores[index];
            scores[index] = scores[i];
            scores[i] = thing;

        }
        for (int i = 0; i < 4; i++)
        {
            result[i] = scores[i][1];
        }
        for(int i = 0; i < result.Length; i++)
        {
            playerList[result[i]].setRank(i);

        }
       
        for (int i = 1; i < result.Length; i++)
        {

            if (scores[i][0] == scores[i-1][0]) //worse rank player has identical score to higher rank player
            {
                playerList[result[i]].setRank(playerList[result[i-1]].getRank());
            }
        }


    }
    public List<Player> getWinners()
    {
        List<Player> result = new List<Player>();
        int max = -1;
        for(int i = 0; i < playerList.Length; i++)
        {
            max = Mathf.Max(max, playerList[i].getScore());
        }
        for(int i = 0; i < playerList.Length; i++)
        {
            if(playerList[i].getScore() == max)
            {
                result.Add(playerList[i]);   
            }
        }
        return result;
    }
    public int getScore(int playerNum)
    {
        return playerList[playerNum].getStars() * 1000 + playerList[playerNum].getCoins();
    }
    public void revealPlayerTabs()
    {
        for (int i = 0; i < playerTabs.Length; i++)
        {
            playerTabs[i].revealUI();
        }
    }
    public void hidePlayerTabs()
    {
        for (int i = 0; i < playerTabs.Length; i++)
        {
            playerTabs[i].hideUI();
        }
    }
    public void resetPlayerSpaceTypes()
    {
        for(int i = 0; i < playerList.Length; i++)
        {
            playerList[i].setSpaceType(2);
        }
    }
    public void saveData()
    {
        for(int i = 0; i < playerList.Length; i++)
        {
            Player playerClass = playerList[i];
            gameData.setCurrentSpace(playerClass.getPlayerNum(),playerClass.getCurrentSpace().name);
            gameData.setCurrentSpaceTag(playerClass.getPlayerNum(), playerClass.getCurrentSpace().tag);
            gameData.setNextSpace(playerClass.getPlayerNum(), playerClass.returnNextSpace().name);
            gameData.setPos(playerClass.getPlayerNum(), playerList[i].gameObject.transform.position);
            gameData.setCoins(playerClass.getPlayerNum(),playerClass.getCoins());
            gameData.setStars(playerClass.getPlayerNum(), playerClass.getStars());
            gameData.setTurnOrder(playerClass.getPlayerNum(), playerClass.getTurnOrder());
        }
        gameData.setBoardState(BACK_FROM_MINIGAME);
        gameData.setGameState(gameState);
    }
}
