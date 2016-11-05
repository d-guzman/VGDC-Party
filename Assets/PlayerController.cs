using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
    //stun
    private bool stunned = false;
    private float timer = 5;
    
    //jump
    private bool isFalling = false;
    private float jumpH=7.0f;

    //attack
    private bool wasHit = false;
    private bool punching = false;

    //body
    private Rigidbody rb1;
    private float speed=5f;
    private MeshRenderer player;
//     private GameObject player;

    void Start()
    {
        //player = GameObject.FindWithTag("p1"); 
        rb1 = GetComponent<Rigidbody>();
        gameObject.SetActive(true);
        
    }

    void Update()
    {
        if (stunned == false)
        {
            //      print(player);
            //Get input from the joystick to 
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVerticle = Input.GetAxis("Vertical");

            if (Input.GetButton("Fire1") && isFalling == false)
            {
                Vector3 movementjump = new Vector3(moveHorizontal * speed * Time.deltaTime, jumpH / 1.1f, moveVerticle * speed * Time.deltaTime);

                rb1.velocity = movementjump;
                isFalling = true;
            }
            if (rb1.velocity[1] == 0)
            { isFalling = false; }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer==0)
            {
                stunned = false;
                punching = false;
            }
            timer = 5;

        }
    }

    void punch()
    {
        if (Input.GetButtonDown("Fire2")&& punching==false)
        {
            print("punch");
            punching = true;
            RaycastHit enemy;
           
            Vector3 half = new Vector3(.5f, .5f, .5f);
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

    void stun()
    {
        
        stunned=true;
        punching = true;
       
    }

    void jumpHit()
    {
        RaycastHit enemy;
        Vector3 half = new Vector3(.5f, .5f, .5f);
        if(Physics.BoxCast(transform.position,half,Vector3.up, out enemy, Quaternion.identity, .1f))
        {
            enemy.collider.attachedRigidbody.AddForce(transform.up * 7, ForceMode.VelocityChange);
            stun();
        }
        
    }

	
  void FixedUpdate()
    {
        punch();
    }

    


   
}
