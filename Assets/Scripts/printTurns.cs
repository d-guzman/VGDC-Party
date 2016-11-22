using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class printTurns : MonoBehaviour {

    // Use this for initialization
    private TurnCounter turnCounter;
    Text myText;
	void Start () {
        myText = gameObject.GetComponent<Text>();
        turnCounter = GameObject.FindGameObjectWithTag("TurnCounter").GetComponent<TurnCounter>();
	}
	
	// Update is called once per frame
	void Update () {
        myText.text = ""+(turnCounter.getTurnCount() -1);
        //print 1 less because the extra turn will be immediately decremented at game start
	}
}
