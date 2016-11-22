using UnityEngine;
using System.Collections;

public class fakeGameData : MonoBehaviour {

    // Use this for initialization
    private GameData gameData;
	void Start () {
        gameData = gameObject.GetComponent<GameData>();
	    for(int i = 0; i < 4; i++)
        {
            gameData.setCoins(i, 10);
            gameData.setStars(i, 0);
        }
	}
	
	
}
