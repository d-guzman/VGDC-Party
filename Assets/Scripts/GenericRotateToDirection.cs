using UnityEngine;
using System.Collections;

public class GenericRotateToDirection : MonoBehaviour {

    // Use this for initialization
    Quaternion targetRotation;
	void Start () {
        targetRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10.0f * Time.deltaTime);
    }
    public void setTargetDirection(Vector3 direction)
    {
        targetRotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
