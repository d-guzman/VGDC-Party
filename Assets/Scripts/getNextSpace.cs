using UnityEngine;
using System.Collections;

public class getNextSpace : MonoBehaviour {

    // Use this for initialization
    public GameObject spaceNext;
    public bool setManually;
	void Start () {
       //print(transform.GetSiblingIndex());
        if (!setManually)
        {
            int index = transform.GetSiblingIndex();


            if (index < transform.parent.childCount)
            {
                GameObject nextBrotherNode = transform.parent.GetChild(index + 1).gameObject;
                spaceNext = nextBrotherNode;
            }
            else
            {
                GameObject nextBrotherNode = transform.parent.GetChild(0).gameObject;
                spaceNext = nextBrotherNode;
            }
        }   
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public GameObject nextSpace()
    {
        return spaceNext;
    }
    void OnDrawGizmosSelected()
    {
       
        Gizmos.color = Color.blue;
        if(spaceNext != null)
        {
            Gizmos.DrawLine(transform.position + Vector3.up * 10, spaceNext.transform.position + Vector3.up * 10);

        }

    }

}
