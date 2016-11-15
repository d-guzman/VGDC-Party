using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DisplayPlayerInfo : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    private Player playerClass;
    private Text starsUI, coinsUI, rankUI;
    private Image img;
    private int stars, coins, rank;
    private float[][] colorList;
    private int color;
	void Start () {
        img = GetComponent<Image>();
        playerClass = player.GetComponent<Player>();
        starsUI = transform.FindChild("StarsUI").GetComponent<Text>();
        coinsUI = transform.FindChild("CoinsUI").GetComponent<Text>();
        rankUI = transform.FindChild("RankUI").GetComponent<Text>();
        color = 0;
        stars = playerClass.getStars();
        coins = playerClass.getCoins();
        rank = playerClass.getRank();
        float[] gray = { 0, 0, 0, (145.0f / 255) };
        float[] blue = { 0, 0, 1, (145.0f / 255) };
        float[] red = { 1, 0, 0, (145.0f /255) };
        colorList = new float[3][];
        colorList[0] = blue;
        colorList[1] = red;
        colorList[2] = gray;


    }
	
	// Update is called once per frame
	void Update () {
        color = playerClass.getSpaceType();
        img.color = new Color(colorList[color][0], colorList[color][1], colorList[color][2], colorList[color][3]);
        stars = playerClass.getStars();
        coins = playerClass.getCoins();
        rank = playerClass.getRank();
        starsUI.text = "x"+stars;
        coinsUI.text = "x" + coins;
        rankUI.text = ""+(rank+1);
    }
}
