using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;




public class startMenu : MonoBehaviour {

    public Button startText;
    public Button exitText;
    public Button increaseTurns;
    public Button decreaseTurns;
    public TurnCounter turnCounter;
    public UIRevealer[] mainMenuElements;
    public UIRevealer[] creditsElements;
    public UIRevealer[] minigameElements;
    public UIRevealer[] turnSelectElements;
    public UIRevealer[] menuScreens;
    private EventSystem eventSystem;

    private bool[] playersReady;
    private int menuState;
    private const int MAIN_MENU = 0;
    private const int TURN_SELECT = 1;
    private const int MINIGAME_SELECT = 2;
    private const int CREDITS = 3;
    private float animationTimer;
    private float screenResetDelay = 0.3f;
    private bool resetScreen = false;
    private int screenToReset = 0;
    private float afterPressDelay = 0.5f;
    // Use this for initialization
    void Start () {
        menuState = 0;
        startText =startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        animationTimer = 2.3f;
    }

    public void ExitPress()
    {
        Application.Quit();
    }

    public void PartyPress()
    {
        menuState = TURN_SELECT;
        eventSystem.SetSelectedGameObject(GameObject.Find("BackButton1"));
        menuScreens[0].revealUI();
        animationTimer = 0.5f;
        afterPressDelay = 0.3f;
    }
    public void startPress()
    {
        SceneManager.LoadScene("gameBoard");
    }
    public void backPress()
    {
        if (afterPressDelay <= 0)
        {
            animationTimer = 0;
            if (menuState == CREDITS)  //screen that called this function
            {
                screenResetDelay = 0.3f;
                resetScreen = true;
                screenToReset = CREDITS;
            } else if(menuState == TURN_SELECT)
            {
                screenResetDelay = 0.3f;
                resetScreen = true;
                screenToReset = TURN_SELECT;
            }
            menuState = MAIN_MENU;
            for (int i = 0; i < menuScreens.Length; i++)
            {
                if (menuScreens[i].revealed)
                {
                    menuScreens[i].hideUI();
                }
            }
            eventSystem.SetSelectedGameObject(GameObject.Find("party mode"));
            afterPressDelay = 0.5f;//prevents button pressing while the menu screen moves
        }

    }
    public void creditsPress()
    {
        if (afterPressDelay <= 0)
        {
            menuState = CREDITS;
            eventSystem.SetSelectedGameObject(null);
            menuScreens[1].revealUI();
            animationTimer = 2.3f;
            eventSystem.SetSelectedGameObject(GameObject.Find("BackButton2"));
            afterPressDelay = 0.5f;//prevents button pressing while the menu screen moves
        }

    }
    public void increaseTurnCount()
    {
        if(turnCounter.getTurnCount() < 100)
        {
            turnCounter.setTurnCount(turnCounter.getTurnCount() + 1);
        }
    }
    public void minigamePress()
    {
        if (afterPressDelay <= 0)
        {
            menuState = MINIGAME_SELECT;
            eventSystem.SetSelectedGameObject(null);
            menuScreens[2].revealUI();
            eventSystem.SetSelectedGameObject(GameObject.Find("BackButton3"));
            afterPressDelay = 0.5f; //prevents button pressing while the menu screen moves
        }

    }
    public void decreaseTurnCount()
    {
        if(turnCounter.getTurnCount() > 1)
        {
            turnCounter.setTurnCount(turnCounter.getTurnCount() - 1);
        }

    }
    // Update is called once per frame
    void Update () {
        
        if(screenResetDelay > 0) //wait for credits screen to leave screen before resetting
        {
           screenResetDelay -= Time.deltaTime;
        } else
        {
            if (resetScreen)
            {
                resetMenuScreen(screenToReset);
                resetScreen = false;
            }
        }
        if(afterPressDelay > 0)
        {
            afterPressDelay -= Time.deltaTime;
            //prevents clicking while screen is moving
        }
        
	    if(menuState == MAIN_MENU)
        {
            //reveal UI sequentially
            if(animationTimer > 0)
            {
                if(animationTimer > 2)
                {
                    mainMenuElements[0].revealUI();
                }
                else if (animationTimer > 1.7f)
                {
                    mainMenuElements[1].revealUI();
                } else if(animationTimer > 1.3f)
                {
                    mainMenuElements[2].revealUI();
                }
                else if(animationTimer > 0.9f)
                {
                    mainMenuElements[3].revealUI();
                }
                else if(animationTimer > 0.5f)
                {
                    mainMenuElements[4].revealUI();
                }
                else if(animationTimer > 0.1f)
                {
                    mainMenuElements[0].revealUI();
                }
                animationTimer -= Time.deltaTime;
            }
        } else if(menuState == TURN_SELECT)
        {
            //wait for time and then reveal
            if(animationTimer > 0)
            {
                animationTimer -= Time.deltaTime;
            } else
            {
                for(int i = 0; i < turnSelectElements.Length; i++)
                {
                    turnSelectElements[i].revealUI();
                }
            }
        }
        else if(menuState == MINIGAME_SELECT)
        {

        } else if(menuState == CREDITS)
        {
            //reveal UI sequentially

            if (animationTimer > 0)
            {
                if (animationTimer > 2)
                {
                    creditsElements[0].revealUI();
                } else if (animationTimer > 1.6f)
                {
                    creditsElements[1].revealUI();
                    creditsElements[6].revealUI();
                } else if (animationTimer > 1.3f)
                {
                    creditsElements[2].revealUI();
                    creditsElements[7].revealUI();
                } else if (animationTimer > 1f)
                {
                    creditsElements[3].revealUI();
                    creditsElements[8].revealUI();
                } else if (animationTimer > 0.7f)
                {
                    creditsElements[4].revealUI();
                    creditsElements[9].revealUI();
                } else if(animationTimer > 0.4f)
                {
                    creditsElements[5].revealUI();
                    creditsElements[10].revealUI();
                }
                animationTimer -= Time.deltaTime;
            }
        }
	}
    public void resetMenuScreen(int screenNum)
    {
        //hides all UI pieces
        if (screenNum == CREDITS)
        {
            for (int i = 0; i < creditsElements.Length; i++)
            {
                creditsElements[i].hideUI();
            }
        } else if(screenNum == TURN_SELECT)
        {
            for(int i = 0; i < turnSelectElements.Length; i++)
            {
                turnSelectElements[i].hideUI();
            }
        }
    }
}
