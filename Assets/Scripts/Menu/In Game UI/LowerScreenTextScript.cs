using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LowerScreenTextScript : MonoBehaviour {

    // Use this for initialization
    Text text;
    public string[] textList;
    private int textIndex;

	void Start () {
        text = GetComponent<Text>();
        textIndex = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("right"))
        {
            textIndex++;
            if(textIndex >= textList.Length)
            {
                textIndex = 0;
            }
        } else if (Input.GetKeyDown("left"))
        {
            textIndex--;
            if (textIndex < 0)
            {
                textIndex = textList.Length-1;
            }
        }
        text.text = textList[textIndex];
	}
    public void setText(int x)
    {
        textIndex = x;
    }
    public string getText(int x)
    {
        return textList[x];
    }
}
