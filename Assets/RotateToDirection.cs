using UnityEngine;
using System.Collections;

public class RotateToDirection : MonoBehaviour
{
    private float playerSpeed = 10.0f;
    // Update is called once per frame
    void Update()
    {
        //Get the input from the joystick
        float moveHori = Input.GetAxisRaw("Horizontal");
        float moveVert = Input.GetAxisRaw("Vertical");

        //Create a vector to apply to the player model based on the joystick input
        Vector3 playerMove = new Vector3(moveHori * playerSpeed * Time.deltaTime, 0.0f, moveVert * playerSpeed * Time.deltaTime);

        //If the player moves the joystick, rotate the character model to be aligned with the movement.
        if (playerMove != Vector3.zero) {
            Quaternion playerRotate = Quaternion.LookRotation(playerMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotate, playerSpeed * Time.deltaTime);
        }
    }
}
