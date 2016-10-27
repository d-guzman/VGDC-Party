﻿using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
    private Rigidbody rb1;
    public float speed;
    void Start()
    {
        rb1 = GetComponent<Rigidbody>();
        
    }

	void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVerticle = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal*speed*Time.deltaTime, 0.0f, moveVerticle*speed*Time.deltaTime);

        transform.Translate(movement);
    }

    void FixedUpdate()
    {
        
        
    }
}