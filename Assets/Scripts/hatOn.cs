using UnityEngine;
using System.Collections;

public class hatOn : MonoBehaviour {
    public GameObject model;
    //Super lazy way to keep the hats above the players!

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position=new Vector3(model.transform.position.x,-3.5f,model.transform.position.z+2);
	}
}
