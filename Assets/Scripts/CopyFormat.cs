using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CopyFormat : MonoBehaviour {

    // Use this for initialization
    public Text copyFrom;
    private Text copyTo;
    private bool haveRealSize;
	void Start () {
        copyTo = GetComponent<Text>();
        haveRealSize = false;
    }
	void Update()
    {  
        copyTo.resizeTextMaxSize = copyFrom.cachedTextGenerator.fontSizeUsedForBestFit;
    }
	
}
