using UnityEngine;
using System.Collections;

public class RotateToDirection : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        if (player.name == "P1_Model")
        {
            var VectorManager = player.GetComponentInParent<PlayerTranslations>();
            if (VectorManager.getPlayer1Move() != Vector3.zero)
            {
                Quaternion player1_Rotate = Quaternion.LookRotation(VectorManager.getPlayer1Move());
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, player1_Rotate, 10.0f * Time.deltaTime);
            }
        }
        if (player.name == "P2_Model")
        {
            var VectorManager = player.GetComponentInParent<PlayerTranslations>();
            if (VectorManager.getPlayer2Move() != Vector3.zero)
            {
                Quaternion player2_Rotate = Quaternion.LookRotation(VectorManager.getPlayer2Move());
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, player2_Rotate, 10.0f * Time.deltaTime);
            }
        }
        if (player.name == "P3_Model")
        {
            var VectorManager = player.GetComponentInParent<PlayerTranslations>();
            if (VectorManager.getPlayer3Move() != Vector3.zero)
            {
                Quaternion player3_Rotate = Quaternion.LookRotation(VectorManager.getPlayer3Move());
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, player3_Rotate, 10.0f * Time.deltaTime);
            }
        }
        if (player.name == "P4_Model")
        {
            var VectorManager = player.GetComponentInParent<PlayerTranslations>();
            if (VectorManager.getPlayer4Move() != Vector3.zero)
            {
                Quaternion player4_Rotate = Quaternion.LookRotation(VectorManager.getPlayer4Move());
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, player4_Rotate, 10.0f * Time.deltaTime);
            }
        }
    }
}
