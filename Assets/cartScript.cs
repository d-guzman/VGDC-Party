using UnityEngine;
using System.Collections;

public class cartScript: MonoBehaviour
{
    
    
    public GameObject train; 
    private string keyword1;
    private string keyword2;
    
    private string masterWord;
    private double timer;
    private double timerCounter;
    float speed = 0;
    float SPEED_INCREASE_INTERVAL = 1f; // how often do you want the speed to change

    // Use this for initialization
    void Start()
    {
        string PString = train.name.Substring(0, 2); 
            keyword1 =PString+ "_Fire1";
            keyword2 = PString+"_Fire2";
        masterWord = keyword1;



    }

    // Update is called once per frame
    void FixedUpdate()
    {

        train.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        SPEED_INCREASE_INTERVAL -= Time.deltaTime;
        if (Input.GetButton(masterWord))
        {
            timer = Time.time;
            timerCounter = Time.time;

            // key change
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
        if (speed < 0)
        { speed = 0f; }
        print(speed);

    }

}

