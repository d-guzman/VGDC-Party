using UnityEngine;
using System.Collections;

public class TextureController : MonoBehaviour {

    public Material blue;
    public Material red;
    public Material star;
    public Material junction;
    public Material white;
    private Material originalMaterial;
    void Start() {
        if (gameObject.CompareTag("BlueSpace"))
        {
            GetComponentInChildren<Renderer>().material = blue;
            originalMaterial = blue;
        }
        else if (gameObject.CompareTag("RedSpace"))
        {
            GetComponentInChildren<Renderer>().material = red;
            originalMaterial = red;
        }
        else if (gameObject.CompareTag("StarSpace"))
        {
            GetComponentInChildren<Renderer>().material = star;
        }
        else if (gameObject.CompareTag("JunctionSpace"))
        {
            GetComponentInChildren<Renderer>().material = junction;
            originalMaterial = junction;
        } 
        else if (gameObject.CompareTag("StartSpace"))
        {
            GetComponentInChildren<Renderer>().material = white;
            originalMaterial = white;
        }
        else
        {
            print("INVALID TAG");
        }
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
        else if (gameObject.CompareTag("StartSpace"))
        {
            GetComponentInChildren<Renderer>().material = white;
        }
        else
        {
            print("INVALID TAG");
        }
    }
    public void applyOriginalTextureAndTag()
    {
        GetComponentInChildren<Renderer>().material = originalMaterial;
        
        if (originalMaterial.name == "BlueSpace")
        {
            transform.tag = "BlueSpace";
        } else if(originalMaterial.name == "RedSpace")
        {
            transform.tag = "RedSpace";
        } else if (originalMaterial.name == "StarSpace")
        {
            transform.tag = "StarSpace";
        } else if(originalMaterial.name == "JunctionSpace")
        {
            transform.tag = "JunctionSpace";
        }
        else if (gameObject.CompareTag("StartSpace"))
        {
            transform.tag = "StartSpace";
        }
    }
}
