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
    private float diceTimer;
    private float totalTime;
    private int result;
    private bool isRolling;
    private float counter;

	void Start () {
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
        if (Input.GetKeyDown("i"))
        {
            rollDice(6, 5);
        }
        scaleDice();
        if (isRolling)
        {
            displayRng();
        }
	}   
    public void rollDice(int result, float duration)
    {
        totalTime = duration;
        diceTimer = duration;
        this.result = result;
        isRolling = true;
    }
    public void displayRng()
    {
        if(diceTimer > 0)
        {
            counter += Time.deltaTime;
            diceTimer -= Time.deltaTime;

            if(diceTimer > 3.5)
            {
                if(counter > 0.08f)
                {
                    counter = 0;
                    displayNewNum(-1);
                }
            } else if(diceTimer > 2.5)
            {
                if (counter > 0.1f)
                {
                    counter = 0;
                    displayNewNum(-1);
                }
            
            }else if(diceTimer > 1.5)
            {
                if (counter > 0.2f)
                {
                    counter = 0;
                    displayNewNum(-1);
                }
            }
            else if(diceTimer > 0.25f)
            {
                if (counter > 0.25f)
                {
                    counter = 0;
                    displayNewNum(result);
                }
            }  else if(diceTimer < 0)
            {
                diceText.text = ""+result;
                isRolling = false;
            }
        }
        
    }
    public void displayNewNum(int blackListNum)
    {
        //random number can't be the blacklist num
        //this is for the very rare occurance that the last random
        //number equals the predetermined result
        string tempNum = "1";
        while (true)
        {
            tempNum = ""+Random.Range(1, 6);
            if (!diceText.text.Equals(tempNum) && !diceText.text.Equals(""+blackListNum))
            {
                break;
            }
        }

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
    public void revealDice(GameObject player)
    {
        revealed = true;
        myRenderer.enabled = true;
        childRenderer.enabled = true;
        transform.position = player.transform.position + Vector3.up * 35;
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
