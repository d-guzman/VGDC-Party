using UnityEngine;
using System.Collections;

public class RandomizeStarSpace : MonoBehaviour {

    // Use this for initialization
    private GameObject[] spaceList;
	void Start () {
        createStarSpace();
        createSpaceList();
        
	}
	
	// Update is called once per frame
	void Update () {
        moveStarSpace();
	}
    public void createStarSpace()
    {
        GameObject[] blueSpaces = GameObject.FindGameObjectsWithTag("BlueSpace");
        GameObject[] redSpaces = GameObject.FindGameObjectsWithTag("RedSpace");
        GameObject[] list = new GameObject[blueSpaces.Length + redSpaces.Length];
        for (int i = 0; i < blueSpaces.Length; i++)
        {
            list[i] = blueSpaces[i];
        }
        for (int i = 0; i < redSpaces.Length; i++)
        {
            list[i + blueSpaces.Length] = redSpaces[i];
        }
        int rng = Random.Range(0, list.Length);
        list[rng].tag = "StarSpace";
        list[rng].GetComponentInChildren<TextureController>().updateTexture();
    }
    public void moveStarSpace()
    {
        int rng = Random.Range(0, spaceList.Length);
        GameObject previousStar = GameObject.FindGameObjectWithTag("StarSpace");
        previousStar.GetComponentInChildren<TextureController>().applyOriginalTextureAndTag();
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
