using UnityEngine;
using System.Collections;

public class CamBehavior : MonoBehaviour {

    GameObject[] players;

    private float closeBy(Vector3 target)
    {
        
        if (Vector3.Distance(transform.position, target) < 0.1)
            {
                //if nearby, snap
                return 1;
            }
            return (1f / 9f);
        
    }
    private bool followPlayer;
    private GameObject targetPlayer;
    private GameObject camera;
    private Vector3 targetLocation;
    private Vector3 offset;
    private Vector3 targetRotation;
    private bool isRotating;
    private float rotSpeed;

    public bool closed
    {
        get
        {
            return (Vector3)transform.position == (Vector3)targetPlayer.transform.position;
        }
    }
    public void setFollowPlayer(bool x)
    {
        followPlayer = x;
    }
    
    public void setTargetPlayer(GameObject x)
    {
        targetPlayer = x;
    }
    public GameObject getTargetPlayer()
    {
        return targetPlayer;
    }
    public void setTargetLocation(Vector3 x)
    {
        targetLocation = x;
    }
    public Vector3 getTargetLocation()
    {
        return targetLocation;
    }
    // Use this for initialization
    void Start()
    {
        followPlayer = false;
        players = GameObject.FindGameObjectsWithTag("Player");
        camera = GameObject.Find("Main Camera");
        //targetPlayer = players[0];
        targetLocation = new Vector3();
        offset = new Vector3(0,90,-50);
        targetRotation = new Vector3(60, 0, 0);
        isRotating = false;
        rotSpeed = 100;
    }

    // Update is called once per frame
    void Update()
    {
        translateCam();
        //rotateCam();
    }
    public void translateCam()
    {
        if (followPlayer)
        {
            transform.position = transform.position + (targetPlayer.transform.position + offset - transform.position) * closeBy(targetPlayer.transform.position + offset);
        }
        else
        {
            transform.position = transform.position + (targetLocation - transform.position) * closeBy(targetLocation);
        }
    }
    /* none of this is working
    public void rotateCam()
    {
        
            Quaternion newRotation = Quaternion.LookRotation(camera.transform + targetRotation, transform.up);
            camera.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotSpeed);
       
        
    }
    public void setTargetRotation(Vector3 target)
    {
        targetRotation = target;
        targetRotation.y = 0;
        targetRotation.z = 0;
        isRotating = true;
        print(targetRotation);

    }
    */
}
