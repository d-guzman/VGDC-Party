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
    public bool jumping = false;
    //attack
    private bool wasHit = false;
    private bool punching = false;
    float punchCooldown;
    public float stunDuration;
    private float stunTimer = 0;
    //movement
    public float playerMaxSpeed = 1000f;
    public float playerAccel = 100f;
    private Vector3 joyStickVector;
    private float joyStickVectorMag;
    private Vector3 horzVelocity;
    //body
    
    private float speed=5f;
    

    private GameStateControl gameStateControl;
    private playerCollisionControl collisionController;
    private string baseString;
    //friction materials
    public PhysicMaterial highFriction;
    public PhysicMaterial lowFriction;
    private Material myMaterial;
    //additional gravity
    public float additionalGravity; //in terms of 1G.  a value of 2 adds double gravity to current gravity, making 3x grav total
    private Rigidbody arm;
    public float punchDuration;
    public float punchTimer;
    public float punchDistance;
    void Start()
    {
        gameObject.SetActive(true);
        gameStateControl = GameObject.FindGameObjectWithTag("MiniGameController").GetComponent<GameStateControl>();
       
        
        
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
        rb = transform.Find(baseString + "_Model").GetComponent<Rigidbody>();
        arm = transform.Find(baseString + "_Arm").GetComponent<Rigidbody>();
        myMaterial = GetComponentInChildren<Renderer>().material;
        
    }

    void FixedUpdate()
    {
        getData();
        preventShoving();

        //print("onGround: " + onGround + ", jumping: " + jumping + ", touchingPlayer: " + touchingPlayer);

        if (gameStateControl.getGameStarted() && !gameStateControl.getGameOver())
        {
            runMovementPhysics();
            
            if (stunned)
            {
                GetComponentInChildren<RotateToDirection>().setDisabled(true);
                if (stunTimer > 0)
                {
                    stunTimer -= Time.deltaTime;
                    myMaterial.SetColor("_Color", Color.red);
                } else
                {
                    stunned = false;
                    myMaterial.SetColor("_Color", Color.white);


                }

            } else
            {
                GetComponentInChildren<RotateToDirection>().setDisabled(false);
                if (Input.GetButtonDown(baseString + "_Fire2"))
                {
                    punch();
                }
            }
        } else
        {
            stunned = false;
            applyFriction();
            GetComponentInChildren<RotateToDirection>().setDisabled(true);

        }





    }
    void Update()
    {
        //must run AFTER fixedUpdate because onGround value is updated during fixedUpdate in PlayerCollisionController
        //Should not cause issues because jumping is a very simple physics interaction without many moving parts
        //this regulates jumping
        getData();

        if (!stunned)
        {
            if (!jumping && onGround)
            {
                if (Input.GetButtonDown(baseString + "_Fire1")) //activate jump
                {
                    jump(jumpVelocity);
                }
            }
            if (collisionController.playerHit())
            {
                setStunned(stunDuration);
            }
        } else
        {
            
        }
        runJumpPhysics();

        
    }



   
    /*
    void punch()
    {
       
        Transform modelTransform = transform.GetChild(0);
        print(modelTransform.name);
        print("punch");

        RaycastHit[] hitList;
           
        Vector3 half = new Vector3(.5f, .2f, 0.5f);
        hitList = Physics.BoxCastAll(modelTransform.position + modelTransform.forward * 1.6f, half, modelTransform.forward, Quaternion.identity, 2f);
        for(int i = 0; i < hitList.Length; i++)
        {
            
            hitList[i].collider.attachedRigidbody.velocity += modelTransform.forward * 10f;
            hitList[i].collider.GetComponentInParent<PlayerController>().setStunned(stunDuration);
            print("Hit Player: " + hitList[i].collider.gameObject.name);
        }
            

                
        
           
        
    }
    */
    
    void punch()
    {
        punchTimer = 0;
        
    }
    
    public void setStunned(float duration)
    {
        //force has already been applied by the player who hits this player
        stunned = true;
        stunTimer = stunDuration;
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
    public void runMovementPhysics()
    {
       
            horzVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            Vector3 moveVector = joyStickVector * playerAccel * Time.deltaTime;
        if (stunned)
        {
            moveVector = Vector3.zero;
        }
            horzVelocity += moveVector;
            if (horzVelocity.magnitude > playerMaxSpeed)
            {
                //if moving too fast, cap to max speed
                horzVelocity = horzVelocity.normalized * playerMaxSpeed;
            }
        

        applyFriction();

        horzVelocity.y = rb.velocity.y;
        rb.velocity = horzVelocity;
        
        
    }
    public void applyFriction()
    {
        if (!stunned)
        {
            if (joyStickVectorMag < 0.1f)
            {
                //if not moving, apply friction
                //this is better than physics friction because it applies in mid air
                horzVelocity *= (1-Time.deltaTime*5);
            }
        } else
        {
            //minimal friction while stunned
            horzVelocity *= (1-Time.deltaTime*1.5f); 

        }
        if (gameStateControl.getGameOver())
        {
            horzVelocity *= (1 - Time.deltaTime * 5);
            horzVelocity.y = rb.velocity.y;
            rb.velocity = horzVelocity;
        }
        
    }
    void jump(float upVelocity)
    {
        jumpTimer = 0f;  //will start counting up from here
        onGround = false;
        jumping = true;
        rb.velocity += Vector3.up * upVelocity;

    }
    public void preventShoving()
    {
        //moving players should not passively shove non-moving players
        //if not  moving, mass increases
        if (onGround && !stunned)
        {
            if (joyStickVectorMag < 0.1f)
            {
                rb.mass = 1000000f;
            }
            else
            {
                rb.mass = 1f;
            }
        } else
        {
            rb.mass = 1f;
        }
        
    }
    
    public void getData()
    {
        //updates variables that store data to be used later in the fixedUpdate()
        float move_Hori = Input.GetAxisRaw(baseString + "_Horizontal");
        float move_Vert = Input.GetAxisRaw(baseString + "_Vertical");

        joyStickVector = new Vector3(move_Hori, 0.0f, move_Vert);
        joyStickVectorMag = joyStickVector.magnitude;
        onGround = collisionController.onGround();
        if (onGround && jumpTimer > 0.1f)
        {
            jumping = false;
        }
        touchingPlayer = collisionController.touchingPlayer();
        jumpButtonHeld = Input.GetButton(baseString + "_Fire1"); //doesn't activate any actions, just needed info
    }
    






    }
