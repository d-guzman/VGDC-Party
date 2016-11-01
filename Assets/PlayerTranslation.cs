using UnityEngine;
using System.Collections;

public class PlayerTranslation : MonoBehaviour {
    private float playerSpeed = 6.0f;
	// Update is called once per frame
	void Update () {
        //Get any input from joystick.
        float moveHori = Input.GetAxisRaw("Horizontal");
        float moveVert = Input.GetAxisRaw("Vertical");

        //Create a vector from the joystick input.
        Vector3 playerMove = new Vector3(moveHori * playerSpeed * Time.deltaTime, 0.0f, moveVert * playerSpeed * Time.deltaTime);

        //Translate the miniPlayer object based on the object, moving the player model.
        transform.Translate(playerMove);
	}
}
