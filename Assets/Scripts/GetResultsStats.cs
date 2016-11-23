using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GetResultsStats : MonoBehaviour
{

    // Use this for initialization
    private int playerNum;
    private int stars;
    private int coins;
    Text starText;
    Text coinText;
    GameData gameData;
    Image avatar;
    private float updateCoinDelay;
    private bool updateCoins;
    UIRevealer addText;
    void Start()
    {
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        try
        {
            gameData.getStars(0);
        }
        catch
        {
            print("no gamedata found");
        }
        stars = gameData.getStars(playerNum);
        coins = gameData.getCoins(playerNum);
        starText = transform.FindChild("StarCount").gameObject.GetComponent<Text>();
        coinText = transform.FindChild("CoinCount").gameObject.GetComponent<Text>();
        avatar = transform.FindChild("PlayerAvatar").gameObject.GetComponent<Image>();
        addText = GameObject.Find("+10Text" + playerNum).GetComponent<UIRevealer>();
        updateData();
    }

    // Update is called once per frame
    void Update()
    {
        if(updateCoinDelay > 0)
        {
            updateCoinDelay -= Time.deltaTime;
            if(updateCoinDelay < 2f && updateCoins)
            {
                addText.gameObject.GetComponent<Text>().enabled = true;
                addText.revealUI();
            }
        } else
        {
            if (updateCoins)
            {
                updateData();
                addText.hideUI();
                addText.gameObject.GetComponent<Text>().enabled = false;
            }
        }
    }
    public void setPlayerNum(int x)
    {
        playerNum = x;
        addText = GameObject.Find("+10Text" + playerNum).GetComponent<UIRevealer>();
        updateData();
    }
    public int getPlayerNum()
    {
        return playerNum;
    }
    public void addCoins(int x)
    {
        updateCoinDelay = 2.5f;
        updateCoins = true;
        gameData.setCoins(playerNum, gameData.getCoins(playerNum) + x);
        
    }
    public void updateData()
    {
        stars = gameData.getStars(playerNum);
        coins = gameData.getCoins(playerNum);
        if (playerNum == 0)
        {
            avatar.color = new Color(1, 0.4f, 0.4f, 1);
        }
        else if (playerNum == 1)
        {
            avatar.color = new Color(1, 1, 0, 1);
        }
        else if (playerNum == 2)
        {
            avatar.color = new Color(1, 1, 1, 1);
        }
        else if (playerNum == 3)
        {
            avatar.color = new Color(0, 0.4f, 1, 1);
        }
        starText.text = "x" + stars;
        coinText.text = "x" + coins;

    }
}
