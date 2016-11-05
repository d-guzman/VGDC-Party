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
    private Vector3 targetLocation;
    private Vector3 offset;

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
        //targetPlayer = players[0];
        targetLocation = new Vector3();
        offset = new Vector3(0,90,-50);

    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            transform.position = transform.position + (targetPlayer.transform.position+offset - transform.position) * closeBy(targetPlayer.transform.position + offset);
        } else
        {
            transform.position = transform.position + (targetLocation - transform.position) * closeBy(targetLocation);
        }

    }
}
