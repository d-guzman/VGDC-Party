using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class dice2d : MonoBehaviour {

    // Use this for initialization
    bool isRolling;
    Text diceText;
    float jumpTimer = 0.3f;
    bool jumping = false;
    float jumpVelocity;
    float counter;
    float gravity;
    float timeBetweenNums;
    private int[] validNumbers;
    private AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip stopSound;
	void Start () {
        isRolling = false;
        diceText = GetComponentInChildren<Text>();
        jumpVelocity = 180;
        counter = 0;
        timeBetweenNums = 0.05f;
        gravity = jumpVelocity * 6f;
    }
	
	// Update is called once per frame
	void Update () {
        if (isRolling)
        {
            displayRng();
        }
        if (jumping)
        {
            jump();
        }
    }
    public void rollDice()
    {
        isRolling = true;
    }
    public void stopDice(int result)
    {
        isRolling = false;
        diceText.text = "" + result;
        jumping = true;
        jumpTimer = 0.3f;
        jumpVelocity = 350f;
        gravity = jumpVelocity * 6f;
        audioSource.PlayOneShot(stopSound);
    }
    public void displayRng()
    {
        counter += Time.deltaTime;
        if (counter > timeBetweenNums)
        {
            counter = 0;
            displayNewNum();
        }
   

    }
    public void displayNewNum()
    {

        string tempNum = "1";

        if (validNumbers.Length == 0)
        {
            print("no valid numbers");
            tempNum = "-1";
            diceText.text = tempNum;
            return;
        } else if(validNumbers.Length == 1)
        {
            print("1 valid number");
            tempNum = ""+validNumbers[0];
            diceText.text = tempNum;
            return;
        }
        while (true)
        {
            int index = Random.Range(0, validNumbers.Length);
            tempNum = "" + validNumbers[index];
            if (!diceText.text.Equals(tempNum))
            {
                break;
            }
        }
        audioSource.PlayOneShot(clickSound);
        diceText.text = tempNum;
    }
    public void jump()
    {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer > 0)
        {
            jumpVelocity -= gravity * Time.deltaTime;
            transform.Translate(Vector3.up * jumpVelocity * Time.deltaTime);
        }
        else
        {
            jumping = false;
        }

    }
    public void setValidNumber(int[] x)
    {
        int[] result = new int[x.Length];
        for(int i = 0; i < x.Length; i++)
        {
            result[i] = x[i] + 1; //player zero becomes player 1, for visuals only
        }
        validNumbers = x;
    }
}
