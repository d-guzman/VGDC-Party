using UnityEngine;
using System.Collections;

public class PlayerTranslations : MonoBehaviour
{
    public float playerSpeed = 6.0f;
    private Vector3 player1_Move;
    private Vector3 player2_Move;
    private Vector3 player3_Move;
    private Vector3 player4_Move;

    public GameObject eventHandler;

    void Update()
    {
        if (eventHandler.GetComponent<GameStateControl>().getGameStarted())
        {
            float move1_Hori = Input.GetAxisRaw("P1_Horizontal");
            float move1_Vert = Input.GetAxisRaw("P1_Vertical");

            float move2_Hori = Input.GetAxisRaw("P2_Horizontal");
            float move2_Vert = Input.GetAxisRaw("P2_Vertical");

            float move3_Hori = Input.GetAxisRaw("P3_Horizontal");
            float move3_Vert = Input.GetAxisRaw("P3_Vertical");

            float move4_Hori = Input.GetAxisRaw("P4_Horizontal");
            float move4_Vert = Input.GetAxisRaw("P4_Vertical");

            player1_Move = new Vector3(move1_Hori * playerSpeed * Time.deltaTime, 0.0f, move1_Vert * playerSpeed * Time.deltaTime);
            player2_Move = new Vector3(move2_Hori * playerSpeed * Time.deltaTime, 0.0f, move2_Vert * playerSpeed * Time.deltaTime);
            player3_Move = new Vector3(move3_Hori * playerSpeed * Time.deltaTime, 0.0f, move3_Vert * playerSpeed * Time.deltaTime);
            player4_Move = new Vector3(move4_Hori * playerSpeed * Time.deltaTime, 0.0f, move4_Vert * playerSpeed * Time.deltaTime);
        }
    }

    public Vector3 getPlayer1Move()
    {
        if (eventHandler.GetComponent<GameStateControl>().getGameOver() == true)
        {
            return Vector3.zero;
        }
        else
        {
            return player1_Move;
        }
    }

    public Vector3 getPlayer2Move()
    {
        if (eventHandler.GetComponent<GameStateControl>().getGameOver() == true)
        {
            return Vector3.zero;
        }
        return player2_Move;
    }

    public Vector3 getPlayer3Move()
    {
        if (eventHandler.GetComponent<GameStateControl>().getGameOver() == true)
        {
            return Vector3.zero;
        }
        return player3_Move;
    }

    public Vector3 getPlayer4Move()
    {
        if (eventHandler.GetComponent<GameStateControl>().getGameOver() == true)
        {
            return Vector3.zero;
        }
        return player4_Move;
    }
}
