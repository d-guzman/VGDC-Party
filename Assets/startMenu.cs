using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class startMenu : MonoBehaviour {

    public Button startText;
    public Button exitText;
     

	// Use this for initialization
	void Start () {
        startText =startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
	
	}

    public void ExitPress()
    {
        Application.Quit();
    }

    public void PartyPress()
    {
        SceneManager.LoadScene(1);
    }
         
	// Update is called once per frame
	void Update () {
	
	}
}
