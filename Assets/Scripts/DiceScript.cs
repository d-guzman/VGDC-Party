using UnityEngine;
using System.Collections;

public class DiceScript : MonoBehaviour {

    // Use this for initialization
    TextMesh diceText;
    MeshRenderer myRenderer;
    MeshRenderer childRenderer;
    bool revealed;
    float scaler;
    public float revealedSize;
    private bool jumping;
    private bool isRolling;
    private float counter;
    private float timeBetweenNums;
    private float jumpTimer;
    private float jumpVelocity;
    private float gravity;
    private AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip stopSound;
	void Start () {
        audioSource = GetComponent<AudioSource>();
        diceText = GetComponentInChildren<TextMesh>();
        myRenderer = GetComponent<MeshRenderer>();
        childRenderer = transform.FindChild("DiceText").GetComponent<MeshRenderer>();

        isRolling = false;
        revealed = false;
        if (!revealed)
        {
            myRenderer.enabled = false;
            childRenderer.enabled = false;
            transform.localScale = new Vector3(0, 0, 0);
        }
        scaler = revealedSize/0.1f;
        counter = 0;
        jumping = false;
        timeBetweenNums = 0.05f;
        jumpTimer = 0f;
        jumpVelocity = 0;
        gravity = 360f;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown("o"))
        {
            revealed = true;
            myRenderer.enabled = true;
            childRenderer.enabled = true;
            transform.localScale = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyDown("p"))
        {
            hideDice();
        }
        if (Input.GetKeyDown("u"))
        {
            rollDice();
        }
        if (Input.GetKeyDown("i"))
        {
            stopDice(2);
        }
        scaleDice();
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
        audioSource.PlayOneShot(stopSound);
        isRolling = false;
        diceText.text = "" + result;
        jumping = true;
        jumpTimer = 0.3f;
        jumpVelocity = 60f;

    }
    public void jump()
    {
        jumpTimer -= Time.deltaTime;
        /*
        if (jumpTimer > 0.05f)
        {
            transform.Translate(Vector3.up * 120 * Time.deltaTime);
        } else if(jumpTimer > 0)
        {
            transform.Translate(Vector3.down * 120 * Time.deltaTime);
        }
        else
        {
            jumpTimer = 0;
            jumping = false;
        }
        */
        if(jumpTimer > 0)
        {
            jumpVelocity -= gravity * Time.deltaTime;
            transform.Translate(Vector3.up * jumpVelocity * Time.deltaTime);
        } else
        {
            jumping = false;
        }
        
    }
    public void displayRng()
    {
        counter += Time.deltaTime;
        if(counter > timeBetweenNums)
        {
            counter = 0;
            displayNewNum();
        }
        /*
        if(diceTimer > 0)
        {
            counter += Time.deltaTime;
            diceTimer -= Time.deltaTime;

            if(diceTimer > 3.5)
            {
                if(counter > 0.08f)
                {
                    counter = 0;
                    displayNewNum();
                }
            } else if(diceTimer > 2.5)
            {
                if (counter > 0.1f)
                {
                    counter = 0;
                    displayNewNum();
                }
            
            }else if(diceTimer > 1.5)
            {
                if (counter > 0.2f)
                {
                    counter = 0;
                    displayNewNum();
                }
            }
            else if(diceTimer > 0.25f)
            {
                if (counter > 0.25f)
                {
                    counter = 0;
                    displayNewNum();
                }
            }  else if(diceTimer < 0)
            {
                diceText.text = ""+result;
                isRolling = false;
            }
        }
        */
        
        
    }
    public void displayNewNum()
    {
        //random number can't be the blacklist num
        //this is for the very rare occurance that the last random
        //number equals the predetermined result
        string tempNum = "1";
        while (true)
        {
            tempNum = ""+Random.Range(1, 6);
            if (!diceText.text.Equals(tempNum))
            {
                break;
            }
        }
        audioSource.PlayOneShot(clickSound);
        diceText.text = tempNum;
    }
    private void scaleDice()
    {
        if (revealed)
        {
            Vector3 growth = new Vector3(scaler, scaler, scaler) * Time.deltaTime;
            transform.localScale += growth;
            if (transform.localScale.x > revealedSize)
            {
                transform.localScale = new Vector3(revealedSize, revealedSize, revealedSize);

            }

        }
        else
        {
            Vector3 shrink = new Vector3(-scaler, -scaler, -scaler) * Time.deltaTime;
            transform.localScale += shrink;
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(0, 0, 0);
                myRenderer.enabled = false;
                childRenderer.enabled = false;
            }

        }
    }
    public void revealDice(Vector3 pos)
    {
        revealed = true;
        myRenderer.enabled = true;
        childRenderer.enabled = true;
        transform.position = pos + Vector3.up * 35;
        transform.localScale = new Vector3(0, 0, 0);
    }
    public void hideDice()
    {
        revealed = false;
    }
    public bool isRevealed()
    {
        return revealed;
    }
    public bool rolling()
    {
        return isRolling;
    }
}
