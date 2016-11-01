using UnityEngine;
using System.Collections;

public class getJunction : MonoBehaviour {

    public GameObject spaceNextPrimary;
    public GameObject spaceNextSecondary;

    public bool setPrimarySpaceManually;
    void Start()
    {
        //print(transform.GetSiblingIndex());

        if (!setPrimarySpaceManually)
        {
            int index = transform.GetSiblingIndex();


            if (index < transform.parent.childCount)
            {
                GameObject nextBrotherNode = transform.parent.GetChild(index + 1).gameObject;
                spaceNextPrimary = nextBrotherNode;
            }
            else
            {
                GameObject nextBrotherNode = transform.parent.GetChild(0).gameObject;
                spaceNextPrimary = nextBrotherNode;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject getPrimarySpace()
    {
        return spaceNextPrimary;
    }
    public GameObject getSecondarySpace()
    {
        return spaceNextSecondary;
    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        if(spaceNextPrimary != null)
        {
            Gizmos.DrawLine(transform.position + Vector3.up * 10, spaceNextPrimary.transform.position + Vector3.up * 10);

        }
        if(spaceNextSecondary != null)
        {
            Gizmos.DrawLine(transform.position + Vector3.up * 10, spaceNextSecondary.transform.position + Vector3.up * 10);

        }


    }
}

