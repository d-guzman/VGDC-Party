using UnityEngine;
using System.Collections;


public class finishLineScript : MonoBehaviour
{
    public GameObject first;
    public GameObject second;
    public GameObject third;
    public GameObject fourth;
    private GameObject[] placement; 
    public GameObject events;
    private int n = 0;

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
        if(placement.Length==4)
        {
            print("game done");
            events.GetComponent<GameStateControl>().setGameOver(true);
        }

        if(Input.GetButtonDown("SubmitStart"))
        {
            events.GetComponent<GameStateControl>().startGame();
            //first.
        }
    }

}
