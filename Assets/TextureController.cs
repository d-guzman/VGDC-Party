using UnityEngine;
using System.Collections;

public class TextureController : MonoBehaviour {

    public Material blue;
    public Material red;
    public Material star;
    public Material junction;

    void Start() {
        updateTexture();
	}
	
	
    public void updateTexture()
    {
        if (gameObject.CompareTag("BlueSpace"))
        {
            GetComponentInChildren<Renderer>().material = blue;
        }
        else if (gameObject.CompareTag("RedSpace"))
        {
            GetComponentInChildren<Renderer>().material = red;
        }
        else if (gameObject.CompareTag("StarSpace"))
        {
            GetComponentInChildren<Renderer>().material = star;
        }
        else if (gameObject.CompareTag("JunctionSpace"))
        {
            GetComponentInChildren<Renderer>().material = junction;
        }
        else
        {
            print("INVALID TAG");
        }
    }
}
