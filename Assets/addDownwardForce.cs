using UnityEngine;
using System.Collections;

public class addDownwardForce : MonoBehaviour {

    // Use this for initialization
    Rigidbody body;
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if(transform.position.y > 75 && transform.position.y < 350)
        {
            body.AddForce(new Vector3(0, -10000f, 0));
        } else
        {
            body.AddForce(new Vector3(0, -1000f, 0));

        }


    }
}
