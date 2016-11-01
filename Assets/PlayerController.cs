using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
    //jump
    private bool isFalling = false;
    private float jumpH=7.0f;

    //attack
    private bool wasHit = false;
    private bool punching = false;

    //body
    private Rigidbody rb1;
    private float speed=5f;

    void Start()
    {
        rb1 = GetComponent<Rigidbody>();
        gameObject.SetActive(true); 
    }

    void Update()
    {
        //Get input from the joystick to 
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVerticle = Input.GetAxis("Vertical");

        if (Input.GetButton("Fire1") && isFalling == false)
        {
            Vector3 movementjump = new Vector3(moveHorizontal * speed * Time.deltaTime, jumpH/1.1f, moveVerticle * speed * Time.deltaTime);

            rb1.velocity = movementjump;
            isFalling = true;
        }
        if (rb1.velocity[1] == 0)
        { isFalling = false; }
    }

    void punch()
    {
        if (Input.GetButtonDown("Fire2")&& punching==false)
        {
            print("punch");
            punching = true;
            RaycastHit enemy;
           
            Vector3 half = new Vector3(.2f, .2f, .2f);
            if (Physics.BoxCast(transform.position, half, transform.forward, out enemy,Quaternion.identity, .7f))
            {
                print("hited");
                enemy.collider.attachedRigidbody.AddForce(transform.forward*7,ForceMode.VelocityChange);

                punching = false;
            }
            else
            { punching = false; }
        }
    }

	
  void FixedUpdate()
    {
        punch();
    }

    


   
}
