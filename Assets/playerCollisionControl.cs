using UnityEngine;
using System.Collections;

public class playerCollisionControl : MonoBehaviour {

    // Use this for initialization
    private bool grounded;
    private bool touchPlayer;
    Rigidbody rb;
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
    void FixedUpdate()
    {
        //important
        //this code runs before events, and Update() runs after

        if (rb.IsSleeping())
        {
            rb.WakeUp();
        }
        grounded = false;
        touchPlayer = false;
    }
    void Update()
    {
        
    }
    // Update is called once per frame
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {  
            grounded = true;
        }
        if (collision.gameObject.CompareTag("PlayerModel"))
        {
            touchPlayer = true;
        }
    }
    
    public bool onGround()
    {
        return grounded;
    }
    public bool touchingPlayer()
    {
        return touchPlayer;
    }
}
