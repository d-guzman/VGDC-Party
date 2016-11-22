using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class printTurns : MonoBehaviour {

    // Use this for initialization
    public TurnCounter turnCounter;
    Text myText;
	void Start () {
        myText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        myText.text = ""+turnCounter.getTurnCount();
	}
}
