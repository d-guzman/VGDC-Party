using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TurnCountUI : MonoBehaviour {

    // Use this for initialization
    private GameObject turnCounter;
    private Text textDisplay;
    private Component counterScript;
	void Start () {
        turnCounter = GameObject.FindGameObjectWithTag("TurnCounter");
        
        textDisplay = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        textDisplay.text = ""+(turnCounter.GetComponent<TurnCounter>().getTurnCount());

	}
}
