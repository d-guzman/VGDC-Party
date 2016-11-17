using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class getResultsStats : MonoBehaviour {

    // Use this for initialization
    private int playerNum;
    private int stars;
    private int coins;
    Text starText;
    Text coinText;
    GameData gameData;
    Image avatar;
	void Start () {
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        stars = gameData.getStars(playerNum);
        coins = gameData.getCoins(playerNum);
        starText = transform.FindChild("StarCount").gameObject.GetComponent<Text>();
        coinText = transform.FindChild("CoinCount").gameObject.GetComponent<Text>();
        avatar = transform.FindChild("PlayerAvatar").gameObject.GetComponent<Image>();
    }

    // Update is called once per frame

    public void setPlayerNum(int x)
    {
        playerNum = x;
        updateData();
    }
    public void updateData()
    {
        stars = gameData.getStars(playerNum);
        coins = gameData.getCoins(playerNum);
        if(playerNum == 0)
        {
            avatar.color = new Color(1, 0.4f, 0.4f, 1);
        } else if(playerNum == 1)
        {
            avatar.color = new Color(1, 1, 0, 1);
        } else if(playerNum == 2)
        {
            avatar.color = new Color(0, 0, 0, 1);
        } else if(playerNum == 3)
        {
            avatar.color = new Color(0, 0.4f, 1, 1);
        }
        starText.text = "x" + stars;
        coinText.text = "x" + coins;

    }
}
