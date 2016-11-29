using UnityEngine;
using System.Collections;

public class RotateToDirection : MonoBehaviour
{
    public GameObject player;
    private string baseString;
    private bool disabled;
    private Quaternion playerRotate;
    private Rigidbody rb;
    void Start()
    {
        disabled = false;
        if (player.name == "P1_Model")
        {
            baseString = "P1";
        }
        else if (player.name == "P2_Model")
        {
            baseString = "P2";
        }
        else if (player.name == "P3_Model")
        {
            baseString = "P3";
        }
        else if (player.name == "P4_Model")
        {
            baseString = "P4";
        }
        rb = GetComponent<Rigidbody>();
        playerRotate = player.transform.rotation;
    }
    void Update()
    {
        
        if (!disabled)
        {
            float move_Hori = Input.GetAxisRaw(baseString + "_Horizontal");
            float move_Vert = Input.GetAxisRaw(baseString + "_Vertical");
            Vector3 playerMove = new Vector3(move_Hori , 0.0f, move_Vert);

            if(playerMove.magnitude > 0.5f && !player.GetComponentInParent<PlayerController>().isStunned())
            {
                playerRotate = Quaternion.LookRotation(playerMove);    
            }
            Quaternion rotation = Quaternion.Slerp(player.transform.rotation, playerRotate, 10.0f * Time.deltaTime);
            rb.MoveRotation(rotation);
        }

    }
    public void setDisabled(bool x)
    {
        disabled = x;
    }
}
