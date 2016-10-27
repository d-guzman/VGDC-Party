﻿using UnityEngine;
using System.Collections;

public class TurnCounter : MonoBehaviour {

    private int turnCount;
    void Awake()
    {
        turnCount = 0;
        DontDestroyOnLoad(transform.gameObject);
    }
    public void setTurnCount(int x)
    {
        turnCount = 0;
    }
    public int getTurnCount()
    {
        return turnCount;
    }
    public bool gameIsOver()
    {
        return turnCount <= 0;
    }
    public void decrementTurnCount()
    {
        turnCount -= 1;
    }
}