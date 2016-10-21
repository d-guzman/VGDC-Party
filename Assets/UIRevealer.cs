﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRevealer : MonoBehaviour {

    // Use this for initialization
    public bool revealed;
    public string anchorDirection;
    public float speedFactor;
    private Vector3 hiddenPosition;
    private Vector3 revealedPosition;
    private bool moving;
   

    void Start() {

        
        /* use speedfactor of 100 for instant movement
         * This doesn't correlate to pixels or time or anything because I'm
         * just done with this script. 
         * 
         * Major resizing of the game window WILL break this 
         * */
        if (speedFactor == 0)
        {
            speedFactor = 1;
        }

        speedFactor = speedFactor / 100;
        if(speedFactor > 1)
        {
            speedFactor = 1;
        }
        revealedPosition = transform.localPosition;
        if (!revealed)
        {
            if (anchorDirection == "Up")
            {

                transform.position += Vector3.up * (1 - gameObject.GetComponent<RectTransform>().anchorMin.y) * Screen.height;
            }
            else if (anchorDirection == "Down")
            {
                transform.position += Vector3.down * gameObject.GetComponent<RectTransform>().anchorMax.y * Screen.height ;

            }
            else if (anchorDirection == "Left")
            {
                transform.position += Vector3.left * gameObject.GetComponent<RectTransform>().anchorMax.x * Screen.width  ;

            }
            else if (anchorDirection == "Right")
            {
                transform.position += Vector3.right * (1-gameObject.GetComponent<RectTransform>().anchorMin.x) * Screen.width  ;

            }
            else
            {
                print("UNKNOWN WALL ANCHOR");
            }


        }

        hiddenPosition = getHiddenPosition();
        moving = false;
    }

    void Update() {
        
        
        if (revealed && moving)
        {
            moveToLocation(revealedPosition);
        } else if (!revealed && moving)
        {
            moveToLocation(hiddenPosition);
        }
        if (Input.GetKeyDown("space"))
        {
            if (revealed)
            {
                hideUI();
            }
            else
            {
                revealUI();
            }
            
       
            
        }
    }
    public void moveToLocation(Vector3 target)
    {
        transform.localPosition += (target - transform.localPosition) * (speedFactor);
        if (Vector3.Distance(transform.localPosition, target) < 1)
        {
            moving = false;
            transform.localPosition = target;
        }
    }
    public void revealUI()
    {
        
        revealed = true;
        moving = true;
        
    }
    public void hideUI()
    {
        
        revealed = false;
        moving = true;
       
    }

    private Vector3 getHiddenPosition()
    {
        if (revealed)
        {
            if (anchorDirection == "Up")
            {

                return transform.localPosition + Vector3.up * (1 - gameObject.GetComponent<RectTransform>().anchorMin.y) * Screen.height;

            }
            else if (anchorDirection == "Down")
            {
                return transform.localPosition + Vector3.down * gameObject.GetComponent<RectTransform>().anchorMax.y * Screen.height;
            }
            else if (anchorDirection == "Left")
            {
                return transform.localPosition + Vector3.left * gameObject.GetComponent<RectTransform>().anchorMax.x * Screen.width;

            }
            else if (anchorDirection == "Right")
            {
                return transform.localPosition + Vector3.right * (1 - gameObject.GetComponent<RectTransform>().anchorMin.x) * Screen.width;

            }
            else
            {
                print("UNKNOWN WALL ANCHOR");
                return transform.localPosition;
            }
        } else
        {
            return transform.localPosition;
        }

    }
    private Vector3 getRevealedPosition()
    {
        if (!revealed)
        {
            if (anchorDirection == "Up")
            {

                return transform.localPosition - Vector3.up * (1 - gameObject.GetComponent<RectTransform>().anchorMin.y) * Screen.height;

            }
            else if (anchorDirection == "Down")
            {
                return transform.localPosition - Vector3.down * gameObject.GetComponent<RectTransform>().anchorMax.y * Screen.height;
            }
            else if (anchorDirection == "Left")
            {
                return transform.localPosition - Vector3.left * gameObject.GetComponent<RectTransform>().anchorMax.x * Screen.width;

            }
            else if (anchorDirection == "Right")
            {
                return transform.localPosition - Vector3.right * (1 - gameObject.GetComponent<RectTransform>().anchorMin.x) * Screen.width;

            }
            else
            {
                print("UNKNOWN WALL ANCHOR");
                return transform.localPosition;
            }
        }
        else
        {
            return transform.localPosition;
        }
    }
    
}
