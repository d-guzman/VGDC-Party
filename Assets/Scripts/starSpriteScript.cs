using UnityEngine;
using System.Collections;

public class starSpriteScript : MonoBehaviour {

    // Use this for initialization
    public float moveHeight;
    public float heightOffset;
    public float moveSpeed;
    private float timer;
    private GameObject starSpace;
    private RandomizeStarSpace starLocation;
	void Start () {
        timer = 0;
        starLocation = GameObject.Find("BoardSpaces").GetComponent<RandomizeStarSpace>();
        starSpace = starLocation.getStarSpace();
	}
	
	// Update is called once per frame
	void Update () {
        starSpace = starLocation.getStarSpace();
        timer += Time.deltaTime * moveSpeed;
        timer = timer % 6.28f;
        transform.position = starSpace.transform.position + Vector3.up * heightOffset + Vector3.up * Mathf.Sin(timer) * moveHeight;
	}
    
}
