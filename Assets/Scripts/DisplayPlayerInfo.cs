using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DisplayPlayerInfo : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    private Player playerClass;
    private Text starsUI, coinsUI, rankUI;

    private int stars, coins, rank;
	void Start () {
        playerClass = player.GetComponent<Player>();
        starsUI = transform.FindChild("StarsUI").GetComponent<Text>();
        coinsUI = transform.FindChild("CoinsUI").GetComponent<Text>();
        rankUI = transform.FindChild("RankUI").GetComponent<Text>();

        stars = playerClass.getStars();
        coins = playerClass.getCoins();
        rank = playerClass.getRank();



    }
	
	// Update is called once per frame
	void Update () {
        stars = playerClass.getStars();
        coins = playerClass.getCoins();
        rank = playerClass.getRank();
        starsUI.text = "x"+stars;
        coinsUI.text = "x" + coins;
        rankUI.text = ""+(rank+1);
    }
}
