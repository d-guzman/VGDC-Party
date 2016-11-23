using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class finishLineScript : MonoBehaviour
{
    public GameObject first;
    public GameObject second;
    public GameObject third;
    public GameObject fourth;
    private GameObject[] placement; 
    public GameObject events;
    private WinnerList winnerList;
    private int n = 0;

    void Start() {
        winnerList = GetComponent<WinnerList>();
        placement = new GameObject[4];
        //events.GetComponent<GameStateControl>();

    }
    //collider
    void OnTriggerEnter(Collider col )
    {
        //Do something like finishing the game or changing the scene
           col.tag = "Point";
           print("finish" + " " +col.name);
           placement[n]=col.gameObject;
        n++;
    }

    GameObject[] getList()
    { return placement; }

    //update checks to see if race is done
    void Update()
    {
        if(placement[0]!=null)
        {
            print(placement[0]);

            print("game done");
            if (!events.GetComponent<GameStateControl>().getGameOver())
            {
                List<int> temp = new List<int>();
                
                if(placement[0].name == "P1_Model")
                {
                    temp.Add(0);
                } else if(placement[0].name == "P2_Model")
                {
                    temp.Add(1);
                }
                else if (placement[0].name == "P3_Model")
                {
                    temp.Add(2);
                }
                else if (placement[0].name == "P4_Model")
                {
                    temp.Add(3);
                }
                winnerList.setWinners(temp);
                events.GetComponent<GameStateControl>().setGameOver(true);

            }

        }

        if(Input.GetButtonDown("SubmitStart"))
        {
            events.GetComponent<GameStateControl>().startGame();
            //first.
        }
    }

}
