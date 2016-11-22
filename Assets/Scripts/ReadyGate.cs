using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ReadyGate : MonoBehaviour
{

    // Use this for initialization
    bool[] playersReady;
    bool allowPlayersReady;
    public Image[] checkList;
    void Start()
    {
        playersReady = new bool[4];
        playersReady[0] = false;
        playersReady[1] = false;
        playersReady[2] = false;
        playersReady[3] = false;
        allowPlayersReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            playerIsReady(0);
        }
        if (Input.GetKeyDown("b"))
        {
            playerIsReady(1);
        }
        if (Input.GetKeyDown("n"))
        {
            playerIsReady(2);
        }
        if (Input.GetKeyDown("m"))
        {
            playerIsReady(3);
        }
        //end keyboard controls

        if (Input.GetButtonDown("P1_Fire1"))
        {
            playerIsReady(0);
        }
        if (Input.GetButtonDown("P2_Fire1"))
        {
            playerIsReady(1);
        }
        if (Input.GetButtonDown("P3_Fire1"))
        {
            playerIsReady(2);
        }
        if (Input.GetButtonDown("P4_Fire1"))
        {
            playerIsReady(3);
        }
    }
    public bool allPlayersReady()
    {
        if (allowPlayersReady)
        {
            bool result = true;
            for (int i = 0; i < playersReady.Length; i++)
            {
                if (!playersReady[i])
                {
                    result = false;
                }
            }
            if (result)
            {
                //sets all back to unready and prevents them from readying until allowed again

                for (int i = 0; i < playersReady.Length; i++)
                {
                    playersReady[i] = false;
                }
                allowPlayersReady = false;
            }
            return result;

        }
        else
        {
            return false;
        }

    }
    public void playerIsReady(int x)
    {
        if (allowPlayersReady)
        {
            playersReady[x] = true;
            checkList[x].enabled = true;
        }
    }
    public bool onePlayerReady()
    {
        bool result = false;

        if (allowPlayersReady)
        {
            for (int i = 0; i < playersReady.Length; i++)
            {
                if (playersReady[i])
                {
                    result = true;
                }
            }
        }
        return result;

    }
    public bool playersCanReady()
    {
        return allowPlayersReady;
    }
    public void allowReadying(bool x)
    {
        allowPlayersReady = x;
    }
    public void unReadyAllPlayers()
    {
        for (int i = 0; i < playersReady.Length; i++)
        {
            playersReady[i] = false;
        }
    }
}