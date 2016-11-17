﻿using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

    // Use this for initialization
    string[] currentSpace;
    string[] nextSpace;
    int[] coins;
    int[] stars;
    Vector3[] pos;
    int[] turnOrder;
    int gameState;
    int boardState;
    string starSpace;

    void Awake () {
        if(GameObject.FindGameObjectsWithTag("GameData").Length > 1)
        {
            DestroyObject(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
        }
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("BoardPlayer");

        currentSpace = new string[4];
        nextSpace = new string[4];
        coins = new int[4];
        stars = new int[4];
        pos = new Vector3[4];
        turnOrder = new int[4];

        for(int i = 0; i < 4; i++)
        {
            currentSpace[i] = "StartSpace";
            coins[i] = 10;
            stars[i] = 0;
            pos[i] = playerList[i].transform.position;
            turnOrder[i] = -1;
            nextSpace[i] = "Space 0";
        }
        gameState = 1;
        boardState = 1;
        starSpace = "";

        //players need current space, coins, stars, x,y
    }
    public int getGameState()
    {
        return gameState;
    }
    public void setGameState(int x)
    {
        gameState = x;

    }
    public int getBoardState()
    {
        return boardState;
    }
    public void setBoardState(int x)
    {
        boardState = x;
    }
    public string getCurrentSpace(int x)
    {
        return currentSpace[x];
    }
    public void setCurrentSpace(int playerNum, string space)
    {
        currentSpace[playerNum] = space;
    }
    public string getNextSpace(int x)
    {
        return nextSpace[x];
    }
    public void setNextSpace(int playerNum, string space)
    {
        nextSpace[playerNum] = space;
    }
    public int getCoins(int x)
    {
        return coins[x];
    }
    public void setCoins(int playerNum, int x)
    {
        coins[playerNum] = x;
    }
    public int getStars(int x)
    {
        return stars[x];
    }
    public void setStars(int playerNum, int x)
    {
        stars[playerNum] = x;
    }
    public Vector3 getPos(int x)
    {
        return pos[x];
    }
    public void setPos(int playerNum, Vector3 newPos)
    {
        pos[playerNum] = newPos;
    }
    public int getTurnOrder(int x)
    {
        return turnOrder[x];
    }
    public void setTurnOrder(int playerNum, int x)
    {
        turnOrder[playerNum] = x;
    }
    public string getStarSpace()
    {
        return starSpace;
    }
    public void setStarSpace(string x)
    {
        starSpace = x;
    }
}
