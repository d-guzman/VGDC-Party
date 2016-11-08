using UnityEngine;
using System.Collections;

public class cart : MonoBehaviour {
    public GameObject player;
   public string keyword1="Fire1";
    public string keyword2 = "Fire2";
    private double timer;
    private double timerCounter;
    float speed =0;
     float SPEED_INCREASE_INTERVAL = 1f; // how often do you want the speed to change
    
    // Use this for initialization
    void Start () {
        




    }

    // Update is called once per frame
    void FixedUpdate () {

        player.transform.Translate(Vector3.forward*speed*Time.deltaTime);
        SPEED_INCREASE_INTERVAL -= Time.deltaTime;
        if(Input.GetButton(keyword1))
        {
            timer = Time.time;
            timerCounter = Time.time;

            // key change
            if (speed < 10)
            {
                speed += .2f;
            }
            

            if (keyword1== "Fire1")
            {
                timer = Time.time;
                timerCounter = Time.time;
                keyword1 = "Fire2";
            }
            else
            {
                keyword1 = "Fire1"; 

            }
       
        }
//speed interval
        if (SPEED_INCREASE_INTERVAL < 0)
        {
            speed -= .3f;
            SPEED_INCREASE_INTERVAL = .7f;
        }
//timer decreases speed
        timerCounter += 1;
        if (timerCounter >= (timer + 75))
        {
            speed -= .7f;
        }
        if(speed < 0)
        { speed = 0f; }
        print(speed);

    }
    
}
