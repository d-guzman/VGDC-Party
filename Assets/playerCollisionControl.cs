using UnityEngine;
using System.Collections;

public class playerCollisionControl : MonoBehaviour {

    // Use this for initialization
    public bool grounded;
    public bool touchPlayer;
    public bool hit;
    Rigidbody rb;
	void Start () {
        rb = GetComponent<Rigidbody>();
        hit = false;
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
        hit = false;
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
        if (collision.gameObject.CompareTag("Arm"))
        {
            hit = true;
            print(collision.transform.forward * 10);
            rb.velocity = collision.transform.forward * 10f;
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
    public bool playerHit()
    {
        return hit;
    }
}
