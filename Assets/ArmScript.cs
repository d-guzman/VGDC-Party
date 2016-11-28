using UnityEngine;
using System.Collections;

public class ArmScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Floor").GetComponent<Collider>(), true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
