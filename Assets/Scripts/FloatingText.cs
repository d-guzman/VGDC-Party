using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {

    // Use this for initialization
    private Camera camera;
    private GameObject player;
    private Transform thisTransform;
    private Vector3 offset;
    private float timer;
    private float riseSpeed;
    private bool hasTarget;
    
	void Awake () {
        hasTarget = false;
        timer = 1.5f;
        riseSpeed = 10f;
        offset = Vector3.up * 10 + Vector3.forward * 10;

        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if(timer > 0)
        {
            offset += Vector3.up * Time.deltaTime * riseSpeed;
            timer -= Time.deltaTime;
        } else
        {
            GameObject.DestroyObject(this.gameObject);
        }
        if (hasTarget)
        {
            transform.position = player.transform.position + offset;
        }
        transform.eulerAngles = new Vector3(60, 0, 0);

        //transform.position = camera.WorldToViewportPoint(playerTransform.position + offset);

    }
    public void setText(string x)
    {
        GetComponent<TextMesh>().text = x;
    }
    public void setPlayer(GameObject player)
    {
        this.player = player;
        this.hasTarget = true;
        print("HEY");
        print(hasTarget);
    }
}
