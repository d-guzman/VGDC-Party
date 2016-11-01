using UnityEngine;
using System.Collections;

public class ArenaEvents : MonoBehaviour {
    public GameObject player1;
    
	// Use this for initialization
	void Start () {
        

    }

	
	// Update is called once per frame
	void Update () {
        GameObject[] playerArray = { player1 };
        //print(playerArray.Length);

        
        if (playerArray[0].activeSelf)
        {
            //print("active");
        }


    }
}
