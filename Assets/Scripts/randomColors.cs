using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class randomColors : MonoBehaviour {

    // Use this for initialization
    Material fontMaterial;
    float[] rngColor;
    float timer;
    public float timeToSwitch;
    public bool smoothColorSwitching;
    Color oldColor, newColor;
    public int colorIndex;
    private Color[] colorList;
	void Start () {
        fontMaterial = GetComponent<MeshRenderer>().material;

        rngColor = new float[3];
        timer = 0;
        if(timeToSwitch == 0)
        {
            timeToSwitch = 0.001f;
        }
        oldColor = fontMaterial.color;
        rngColor[0] = Random.value;
        rngColor[1] = Random.value;
        rngColor[2] = Random.value;
        newColor = new Color(rngColor[0], rngColor[1], rngColor[2]);
        colorList = new Color[6];
        if (smoothColorSwitching)
        {
            oldColor = colorList[colorIndex];
            newColor = colorList[colorIndex + 1];
        }
        colorList[0] = Color.red;
        colorList[1] = Color.magenta;
        colorList[2] = Color.blue;
        colorList[3] = Color.cyan;
        colorList[4] = Color.green;
        colorList[5] = Color.yellow;
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
            if (smoothColorSwitching)
            {
                colorIndex += 1;
                if (colorIndex >= colorList.Length)
                {
                    colorIndex = 0;
                }
                newColor = getNextColor(colorIndex);
            }
            else
            {
                newColor = getNewColor();
            }
        }
        fontMaterial.color = (Color.Lerp(oldColor, newColor, timer / timeToSwitch));



    }
    public Color getNewColor()
    {
        rngColor[0] = Random.value;
        rngColor[1] = Random.value;
        rngColor[2] = Random.value;
        return new Color(rngColor[0], rngColor[1], rngColor[2]);
    }
    public Color getNextColor(int x)
    {
        return colorList[x];
    }
}
