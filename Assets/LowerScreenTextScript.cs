﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LowerScreenTextScript : MonoBehaviour {

    // Use this for initialization
    Text text;
    public string[] textList;
    private int textIndex;
    private int timer;

	void Start () {
        text = GetComponent<Text>();
        textIndex = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void timedReveal(float seconds)
    {

    }
    public void revealUntilButton()
    {

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
