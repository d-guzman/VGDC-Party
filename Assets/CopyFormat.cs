using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CopyFormat : MonoBehaviour {

    // Use this for initialization
    public Text copyFrom;
    private Text copyTo;
	void Start () {
        copyTo = GetComponent<Text>();
    }
	void Update()
    {
        copyTo.fontSize = copyFrom.cachedTextGenerator.fontSizeUsedForBestFit;
    }
	
}
