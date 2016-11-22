using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class randomColors : MonoBehaviour {

    // Use this for initialization
    TextMesh text;
    float[] rngColor;
    float timer;
    public float timeToSwitch;
    Color oldColor, newColor;
	void Start () {
        text = GetComponent<TextMesh>();
        rngColor = new float[3];
        timer = 0;
        if(timeToSwitch == 0)
        {
            timeToSwitch = 0.001f;
        }
        oldColor = text.color;
        rngColor[0] = Random.value;
        rngColor[1] = Random.value;
        rngColor[2] = Random.value;
        newColor = new Color(rngColor[0], rngColor[1], rngColor[2]);
    }
	
	// Update is called once per frame
	void Update () {
        if(timer < timeToSwitch)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            oldColor = newColor;
            newColor = getNewColor();
        }
        text.color = Color.Lerp(oldColor, newColor, timer / timeToSwitch);


    }
    public Color getNewColor()
    {
        rngColor[0] = Random.value;
        rngColor[1] = Random.value;
        rngColor[2] = Random.value;
        return new Color(rngColor[0], rngColor[1], rngColor[2]);
    }
}
