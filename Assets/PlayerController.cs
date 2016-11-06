﻿using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
    //stun
    public bool stunned = false;
    private float timer = 5;
    
    //jump
    public bool isFalling = false;
    private float jumpH=7.0f;

    //attack
    public bool wasHit = false;
    public bool punching = false;

    //body
    
    private float speed=5f;
    public GameObject player;
  
    
    void Start()
    {
        gameObject.SetActive(true);
        
    }

    void Update()
    {
        
      

            if (player.name == "P1_Model")
        {
            if (stunned == false)
            {

                float moveHorizontal = Input.GetAxis("P1" + "_Horizontal");
                float moveVerticle = Input.GetAxis("P1" + "_Vertical");

                if (Input.GetButton("P1" + "_Fire1") && isFalling == false)
                {
                    Vector3 movementjump = new Vector3(moveHorizontal * speed * Time.deltaTime, jumpH / 1.1f, moveVerticle * speed * Time.deltaTime);
                    player.GetComponent<Rigidbody>().velocity = movementjump;
                    isFalling = true;
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
            punch("P1");
        }

        if (player.name == "P2_Model")
        {
            
            if (stunned == false)
            {

                float moveHorizontal = Input.GetAxis("P2" + "_Horizontal");
                float moveVerticle = Input.GetAxis("P2" + "_Vertical");

                if (Input.GetButton("P2" + "_Fire1") && isFalling == false)
                {
                    Vector3 movementjump = new Vector3(moveHorizontal * speed * Time.deltaTime, jumpH / 1.1f, moveVerticle * speed * Time.deltaTime);
                    player.GetComponent<Rigidbody>().velocity = movementjump;
                    isFalling = true;
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

            punch("P2");

        }

        if (player.name == "P3_Model")
        {
            if (stunned == false)
            {

                float moveHorizontal = Input.GetAxis("P3" + "_Horizontal");
                float moveVerticle = Input.GetAxis("P3" + "_Vertical");

                if (Input.GetButton("P3" + "_Fire1") && isFalling == false)
                {
                    Vector3 movementjump = new Vector3(moveHorizontal * speed * Time.deltaTime, jumpH / 1.1f, moveVerticle * speed * Time.deltaTime);
                    player.GetComponent<Rigidbody>().velocity = movementjump;
                    isFalling = true;
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

            punch("P3");

        }

        if (player.name == "P4_Model")
        {
            
            if (stunned == false)
            {

                float moveHorizontal = Input.GetAxis("P4" + "_Horizontal");
                float moveVerticle = Input.GetAxis("P4" + "_Vertical");

                if (Input.GetButton("P4" + "_Fire1") && isFalling == false)
                {
                    Vector3 movementjump = new Vector3(moveHorizontal * speed * Time.deltaTime, jumpH / 1.1f, moveVerticle * speed * Time.deltaTime);
                    player.GetComponent<Rigidbody>().velocity = movementjump;
                    isFalling = true;
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

    void jumpHit(string x)
    {
        
    }

	
 

    


   
}
