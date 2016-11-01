using UnityEngine;
using System.Collections;

public class RotateToDirection : MonoBehaviour {

    // Use this for initialization
    private bool rotating;
    private Quaternion toAngle;
    private Quaternion fromAngle;
    public float speed;
    private float t;
	void Start () {
        fromAngle = transform.rotation;
        toAngle = transform.rotation;
        rotating = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (rotating)
        {
            if (t < 1)
            {
                t += Time.deltaTime * speed;
            } else
            {
                rotating = false;
                t = 1;
            }
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
        } 
        

    }
    public void setAngle(Quaternion x)
    {
        toAngle = x;
        t = 0;
        rotating = true;
    }

}
