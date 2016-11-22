using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class checkBoxOffset : MonoBehaviour {

    // Use this for initialization
    public Text text;
    private float offset;
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
        offset = text.preferredWidth;
        print(offset);
        transform.position = text.transform.position + Vector3.right * offset;
	}
}
