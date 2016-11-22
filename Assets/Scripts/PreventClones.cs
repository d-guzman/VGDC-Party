using UnityEngine;
using System.Collections;

public class PreventClones : MonoBehaviour {

    // Use this for initialization
    public string tag;
	void Start () {
        
        if (GameObject.FindGameObjectsWithTag(tag).Length > 1)
        {
            DestroyObject(this.gameObject);
        }
        
    }
	
	// Update is called once per frame

}
