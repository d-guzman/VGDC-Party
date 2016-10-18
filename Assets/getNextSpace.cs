using UnityEngine;
using System.Collections;

public class getNextSpace : MonoBehaviour {

    // Use this for initialization
    public GameObject nextSpace;

	void Start () {
        



        print(transform.GetSiblingIndex());
        int index = transform.GetSiblingIndex();
        
        
        if (index < transform.parent.childCount-1)
        {
            GameObject nextBrotherNode = transform.parent.GetChild(index + 1).gameObject;
            nextSpace = nextBrotherNode;
        } else
        {
            GameObject nextBrotherNode = transform.parent.GetChild(0).gameObject;
            nextSpace = nextBrotherNode;
        }
        


    }
	
	// Update is called once per frame
	void Update () {
        
    }
    
}
