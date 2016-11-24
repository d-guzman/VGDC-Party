using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
    //stun
    public bool stunned = false;
    private float timer = 5;
    
    //jump
    // --- isFalling IS A DEPRECIATED VARIABLE. DELETE LATER! 
    public bool isFalling = false;
    private float jumpH=7.0f;
    private Rigidbody rb;
    public int maxJumpFrames = 15;
    private int currentJumpFrames;

    //attack
    public bool wasHit = false;
    public bool punching = false;

    //body
    
    private float speed=5f;
    public GameObject player;

    private GameStateControl gameStateControl;
    private string baseString;

    void Start()
    {
        gameObject.SetActive(true);
        gameStateControl = GameObject.FindGameObjectWithTag("MiniGameController").GetComponent<GameStateControl>();
        baseString = "P"+gameObject.name.Substring(1, 1);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (gameStateControl.getGameStarted() && !gameStateControl.getGameOver())
        {

            if (player.name == "P1_Model")
            {

                if (stunned == false)
                {
                    jumpHit();

                    float moveHorizontal = Input.GetAxis("P1" + "_Horizontal");
                    float moveVerticle = Input.GetAxis("P1" + "_Vertical");

                    // This is the new jumping mechanic. It works by keeping track of a specific
                    // amount of frames, and stops jumping when you hit the limit. The frame tracker
                    // also prevents jump spamming.
                    if (currentJumpFrames < maxJumpFrames && nowFalling() == false && Input.GetButton("P1_Fire1"))
                    {
                        rb.useGravity = false;
                        rb.velocity = Vector3.up * 6.5f;
                        currentJumpFrames++;
                    }
                    else
                    {
                        if (currentJumpFrames != 0)
                        {
                            rb.useGravity = true;
                            rb.velocity = -Vector3.up * 6.0f;
                            currentJumpFrames--;
                        }
                    }
                    //

                    if (player.GetComponent<Rigidbody>().velocity[1] == 0)
                    { isFalling = false; }
                }
                else
                {
                    player.GetComponent<Collider>().attachedRigidbody.AddForce(-player.transform.forward, ForceMode.VelocityChange);
                    stunned = false;
                    punching = false;

                }
                punch("P1");
            }

            if (player.name == "P2_Model")
            {


                if (stunned == false)
                {
                    jumpHit();

                    float moveHorizontal = Input.GetAxis("P2" + "_Horizontal");
                    float moveVerticle = Input.GetAxis("P2" + "_Vertical");
                    // New Jump Mechanic!
                    if (currentJumpFrames < maxJumpFrames && nowFalling() == false && Input.GetButton("P2_Fire1"))
                    {
                        rb.useGravity = false;
                        rb.velocity = Vector3.up * 6.5f;
                        currentJumpFrames++;
                    }
                    else
                    {
                        if (currentJumpFrames != 0)
                        {
                            rb.useGravity = true;
                            rb.velocity = -Vector3.up * 6.0f;
                            currentJumpFrames--;
                        }
                    }
                    //
                    if (player.GetComponent<Rigidbody>().velocity[1] == 0)
                    { isFalling = false; }
                }
                else
                {

                    player.GetComponent<Collider>().attachedRigidbody.AddForce(-player.transform.forward, ForceMode.VelocityChange);
                    stunned = false;
                    punching = false;

                }

                punch("P2");

            }

            if (player.name == "P3_Model")
            {
                if (stunned == false)
                {
                    jumpHit();

                    float moveHorizontal = Input.GetAxis("P3" + "_Horizontal");
                    float moveVerticle = Input.GetAxis("P3" + "_Vertical");
                    // New Jump Mechanic!
                    if (currentJumpFrames < maxJumpFrames && nowFalling() == false && Input.GetButton("P3_Fire1"))
                    {
                        rb.useGravity = false;
                        rb.velocity = Vector3.up * 6.5f;
                        currentJumpFrames++;
                    }
                    else
                    {
                        if (currentJumpFrames != 0)
                        {
                            rb.useGravity = true;
                            rb.velocity = -Vector3.up * 6.0f;
                            currentJumpFrames--;
                        }
                    }
                    //
                    if (player.GetComponent<Rigidbody>().velocity[1] == 0)
                    { isFalling = false; }
                }
                else
                {
                    player.GetComponent<Collider>().attachedRigidbody.AddForce(-player.transform.forward, ForceMode.VelocityChange);
                    stunned = false;
                    punching = false;

                }

                punch("P3");

            }

            if (player.name == "P4_Model")
            {

                if (stunned == false)
                {
                    jumpHit();

                    float moveHorizontal = Input.GetAxis("P4" + "_Horizontal");
                    float moveVerticle = Input.GetAxis("P4" + "_Vertical");

                    /* OLD JUMP MECHANIC
                    if (Input.GetButtonDown("P4" + "_Fire1") && isFalling == false)
                    {
                        Vector3 movementjump = new Vector3(moveHorizontal * speed * Time.deltaTime, jumpH / 1.1f, moveVerticle * speed * Time.deltaTime);
                        player.GetComponent<Rigidbody>().velocity = movementjump;
                        isFalling = true;
                    }
                    */
                    if (currentJumpFrames < maxJumpFrames && nowFalling() == false && Input.GetButton("P4_Fire1"))
                    {
                        rb.useGravity = false;
                        rb.velocity = Vector3.up * 6.5f;
                        currentJumpFrames++;
                    }
                    else
                    {
                        if (currentJumpFrames != 0)
                        {
                            rb.useGravity = true;
                            rb.velocity = -Vector3.up * 6.0f;
                            currentJumpFrames--;
                        }
                    }
                    if (player.GetComponent<Rigidbody>().velocity[1] == 0)
                    { isFalling = false; }
                }
                else
                {
                    player.GetComponent<Collider>().attachedRigidbody.AddForce(-player.transform.forward, ForceMode.VelocityChange);
                    stunned = false;
                    punching = false;

                }

                punch("P4");

            }
            
        }
    }



   

    void punch(string x)
    {
        if (Input.GetButtonDown(x+"_Fire2")&& punching==false)
        {
            print("punch");
            punching = true;
            RaycastHit enemy;
           
            Vector3 half = new Vector3(.6f, .6f, .6f);
            if (Physics.BoxCast(player.transform.position, half, player.transform.forward, out enemy,Quaternion.identity, 1f))
            {
                print("hited");
                enemy.collider.attachedRigidbody.AddForce(player.transform.forward*7,ForceMode.VelocityChange);

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
        if (Physics.Raycast(player.transform.position, Vector3.up, out enemy,.7f))
        {
            enemy.collider.attachedRigidbody.AddForce((enemy.transform.up)*4+enemy.transform.forward, ForceMode.VelocityChange);
            stun();
        }
    }

    private bool nowFalling() {
        if (rb.velocity.y < -.001f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    

	
 

    


   
}
