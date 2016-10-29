using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
    //jump
    private bool isFalling = false;
    private int jumpH=7;

    //attack
    private bool wasHit = false;
     




    //body
    private Rigidbody rb1;
    private float speed=5f;
    void Start()
    {
        rb1 = GetComponent<Rigidbody>();
        gameObject.SetActive(true);
        
    }

    void punch()
    {
        Vector3 half = new Vector3(.2f, .2f, .2f);
        if(Physics.BoxCast(transform.position,half,transform.forward,Quaternion.identity,.7f) )
        {
            print("hited");
        }
    }

	void Update()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVerticle = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal*speed*Time.deltaTime, 0.0f, moveVerticle*speed*Time.deltaTime);
        
        transform.Translate(movement);
        //print(rb1.velocity[1]);
       // print(isFalling);
        if (Input.GetButton("Fire1") && isFalling==false)
        {
            Vector3 movementjump = new Vector3(moveHorizontal * speed * Time.deltaTime, jumpH, moveVerticle * speed * Time.deltaTime);

            rb1.velocity = movementjump;
            isFalling = true;
            //print(isFalling);
        }
        if (rb1.velocity[1] == 0)
        { isFalling = false; }

        punch();

    }

    


   
}
