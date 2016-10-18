using UnityEngine;
using System.Collections;

public class RandomizeStarSpace : MonoBehaviour {

    // Use this for initialization
    private GameObject[] spaceList;
	void Start () {
        createSpaceList();
        
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void moveStarSpace()
    {
        int rng = Random.Range(0, spaceList.Length);
        GameObject previousStar = GameObject.FindGameObjectWithTag("StarSpace");
        previousStar.tag = spaceList[rng].tag;
        previousStar.GetComponentInChildren<TextureController>().updateTexture();
        spaceList[rng].tag = "StarSpace";
        spaceList[rng].GetComponentInChildren<TextureController>().updateTexture();
        spaceList[rng] = previousStar;
    }
    public void createSpaceList()
    {
        GameObject[] blueSpaces = GameObject.FindGameObjectsWithTag("BlueSpace");
        GameObject[] redSpaces = GameObject.FindGameObjectsWithTag("RedSpace");
        spaceList = new GameObject[blueSpaces.Length + redSpaces.Length];
        for (int i = 0; i < blueSpaces.Length; i++)
        {
            spaceList[i] = blueSpaces[i];          
        }
        for (int i = 0; i < redSpaces.Length; i++)
        {
            spaceList[i + blueSpaces.Length] = redSpaces[i]; 
        }
        
    }
}
