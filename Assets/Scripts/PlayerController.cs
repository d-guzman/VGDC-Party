using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
    private int playerNum;  //which player this represents [0,1,2,3]
    //stun
    private bool stunned = false;
    private float timer = 5;
    bool touchingPlayer = false;
    //jump
    // --- isFalling IS A DEPRECIATED VARIABLE. DELETE LATER! 
    private bool isFalling = false;
    public float jumpVelocity=10f;
    private Rigidbody rb;
    float jumpTimer;
    public float grav1Duration;
    public bool onGround = true;
    bool jumpButtonHeld = false;
    bool inAir = false;
    bool jumping = false;
    //attack
    private bool wasHit = false;
    private bool punching = false;
    float punchCooldown;
    //body
    
    private float speed=5f;
    

    private GameStateControl gameStateControl;
    private playerCollisionControl collisionController;
    private string baseString;
    //friction materials
    public PhysicMaterial highFriction;
    public PhysicMaterial lowFriction;
    //additional gravity
    public float additionalGravity; //in terms of 1G.  a value of 2 adds double gravity to current gravity, making 3x grav total
    void Start()
    {
        gameObject.SetActive(true);
        gameStateControl = GameObject.FindGameObjectWithTag("MiniGameController").GetComponent<GameStateControl>();
        rb = GetComponentInChildren<Rigidbody>();
        collisionController = GetComponentInChildren<playerCollisionControl>();
        if(gameObject.name == "Player1")
        {
            playerNum = 0;
            baseString = "P1";
        } else if(gameObject.name == "Player2")
        {
            playerNum = 1;
            baseString = "P2";
        } else if(gameObject.name == "Player3")
        {
            playerNum = 2;
            baseString = "P3";
        } else if(gameObject.name == "Player4")
        {
            playerNum = 3;
            baseString = "P4";
        }
    }

    void Update()
    {
        onGround = collisionController.onGround();
        if (onGround)
        {
            jumping = false;
        }
        touchingPlayer = collisionController.touchingPlayer();
        
        //print("onGround: " + onGround + ", jumping: " + jumping + ", touchingPlayer: " + touchingPlayer);
        jumpButtonHeld = Input.GetButton(baseString + "_Fire1"); //doesn't activate any actions, just needed info
        if (gameStateControl.getGameStarted() && !gameStateControl.getGameOver())
        {
            
            if (stunned == false)
            {
                runJumpPhysics();
                if(!jumping && onGround)
                {
                    if (Input.GetButtonDown(baseString + "_Fire1")) //activate jump
                    {
                        jump(jumpVelocity);
                    }
                }
                jumpHit();
                float moveHorizontal = Input.GetAxis(baseString + "_Horizontal");
                float moveVerticle = Input.GetAxis(baseString + "_Vertical");
                
            }
            else
            {
               rb.AddForce(-transform.forward, ForceMode.VelocityChange);
                stunned = false;
                punching = false;
                jumpButtonHeld = false;
            }
            punch(baseString);
            

            


               

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
            if (Physics.BoxCast(transform.position, half, transform.forward, out enemy,Quaternion.identity, 1f))
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
        if (Physics.Raycast(transform.position, Vector3.up, out enemy,.7f))
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
    
    void runJumpPhysics()
    {
        if (jumping)
        {
            jumpTimer += Time.deltaTime;
            if(jumpTimer < grav1Duration && jumpButtonHeld)
            {
                //use normal gravity while within grav1Duration AND player is holding down jump
                //add no additional gravity for part one of jump
                print("normal gravity");
            } else
            {
                //use higher gravity after grav1Duration expires OR player stops holding jump
                //add additional gravity
                rb.AddForce(Physics.gravity * additionalGravity * rb.mass);
                print("high gravity");
            }
        }
        
    }
    void jump(float upForce)
    {
        jumpTimer = 0f;  //will start counting up from here
        onGround = false;
        jumping = true;
        rb.position = rb.position + Vector3.up * 0.1f;
        rb.velocity = Vector3.up * jumpVelocity;
        print("PLAYER JUMPED");
    }








    }
