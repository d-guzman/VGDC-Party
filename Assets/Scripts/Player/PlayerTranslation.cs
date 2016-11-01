using UnityEngine;
using System.Collections;

public class PlayerTranslation : MonoBehaviour {
    private float playerSpeed = 5.0f;
	// Update is called once per frame
	void Update () {
        float moveHori = Input.GetAxisRaw("Horizontal");
        float moveVert = Input.GetAxisRaw("Vertical");

        Vector3 playerMove = new Vector3(moveHori * playerSpeed * Time.deltaTime, 0.0f, moveVert * playerSpeed * Time.deltaTime);

        transform.Translate(playerMove);
	}
}
