using UnityEngine;
using System.Collections;

public class cart : MonoBehaviour
{
    public GameObject train;
    private string keyword1;                        //  P#_Fire1
    private string keyword2;                        // P#_Fire2
    private string masterWord;                      // switches between keywords
    private double timer;                           // these two timers will be compared to see how much time has past between button press
    private double timerCounter;
    private float speed = 0;                        // changable speed
    private float SPEED_INCREASE_INTERVAL = 1f;     // how often do you want the speed to change
    public bool moveActive = true;
    
    // Use this for initialization
    void Start()
    {
        string PString = train.name.Substring(0, 2);
        keyword1 = PString + "_Fire1";
        keyword2 = PString + "_Fire2";
        masterWord = keyword1;



    }

    // Update is called once per frame
    void FixedUpdate()
    {

        train.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        SPEED_INCREASE_INTERVAL -= Time.deltaTime;
        if (Input.GetButtonDown(masterWord))
        {
            timer = Time.time;
            timerCounter = Time.time;

            // key change and speed increase
            if (speed < 10)
            {
                speed += .2f;
            }


            if (masterWord == keyword1)
            {
                timer = Time.time;
                timerCounter = Time.time;
                masterWord = keyword2;
            }
            else
            {
                timer = Time.time;
                timerCounter = Time.time;
                masterWord = keyword1;

            }

        }
        if ( Input.GetButtonDown(keyword1)&&Input.GetButtonDown(keyword2))
        { speed = 0; }
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
            speed -= .8f;
        }
        if (speed < 0 || moveActive==false)
        { speed = 0f; }
        //print(speed);
         
        //make movement inactive
        if(train.tag == "Point")
        { moveActive = false; }
        
    }

}

