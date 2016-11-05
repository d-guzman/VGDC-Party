using UnityEngine;
using System.Collections;

public class PlayerTranslation : MonoBehaviour
{
    //Gets the player that the script is connected to.
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player.name == "Player1")
        {
            var vectorManager = player.GetComponentInParent<PlayerTranslations>();
            player.transform.Translate(vectorManager.getPlayer1Move());
        }

        if (player.name == "Player2")
        {
            var vectorManager = player.GetComponentInParent<PlayerTranslations>();
            player.transform.Translate(vectorManager.getPlayer2Move());
        }

        if (player.name == "Player3")
        {
            var vectorManager = player.GetComponentInParent<PlayerTranslations>();
            player.transform.Translate(vectorManager.getPlayer3Move());
        }

        if (player.name == "Player4")
        {
            var vectorManager = player.GetComponentInParent<PlayerTranslations>();
            player.transform.Translate(vectorManager.getPlayer4Move());
        }
    }
}
