﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DeathZoneScript : MonoBehaviour
{
    // Connect the Player objects to make it easier to deactivate them, and
    // create corresponding boolean values to make sure that we know when they
    // are out of play.
    public GameObject player1;
    private bool P1_Out = false;

    public GameObject player2;
    private bool P2_Out = false;

    public GameObject player3;
    private bool P3_Out = false;

    public GameObject player4;
    private bool P4_Out = false;

    private GameObject[] playerList;

    public GameObject eventHandler;
    private WinnerList winnerList;

    // If the the player model enters the death zone, the player GameObject will 
    // become deactivated.
    void Start()
    {
        playerList = GetActivePlayers();
        winnerList = GetComponent<WinnerList>();
    }
    void Update()
    {
        if (playerList.Length == 1)
        {
            if (eventHandler.GetComponent<GameStateControl>().getGameOver() == false)
            {
                List<int> temp = new List<int>();
                string winner = playerList[0].name;
                winner = winner.Substring(6);
              
                temp.Add((int.Parse(winner) - 1));
                winnerList.setWinners(temp);
                eventHandler.GetComponent<GameStateControl>().setGameOver(true);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "P1_Model")
        {
            player1.SetActive(false);
            P1_Out = true;
            playerList = GetActivePlayers();
            Debug.Log("Player 1 is out!");
        }

        if (other.name == "P2_Model")
        {
            player2.SetActive(false);
            P2_Out = true;
            playerList = GetActivePlayers();
            Debug.Log("Player 2 is out!");
        }

        if (other.name == "P3_Model")
        {
            player3.SetActive(false);
            P3_Out = true;
            playerList = GetActivePlayers();
            Debug.Log("Player 3 is out!");
        }

        if (other.name == "P4_Model")
        {
            player4.SetActive(false);
            P4_Out = true;
            playerList = GetActivePlayers();
            Debug.Log("Player 4 is out!");
        }
    }
    // --- POSSIBLY USEFUL PUBLIC FUNCTIONS
    // GetActivePlayers - Returns a new list of the active players.
    public GameObject[] GetActivePlayers()
    {
        return GameObject.FindGameObjectsWithTag("Player");
    }

    // IsPlayer#Out - Returns True if a player is deactive (therefore out),
    //                otherwise it returns false.
    public bool IsPlayer1Out()
    {
        return P1_Out;
    }

    public bool IsPlayer2Out()
    {
        return P2_Out;
    }

    public bool IsPlayer3Out()
    {
        return P3_Out;
    }

    public bool IsPlayer4Out()
    {
        return P4_Out;
    }
}



