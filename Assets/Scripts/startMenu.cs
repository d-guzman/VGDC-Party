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
    private TurnCounter turnCounter;
    public UIRevealer blackPanelUp;
    public UIRevealer blackPanelDown;
    public UIRevealer[] mainMenuElements;
    public UIRevealer[] creditsElements;
    public UIRevealer[] minigameElements;
    public UIScroller[] minigameList;
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
    int minigameSelected;
    bool revealMinigame;
    bool loadMinigame;
    bool loadBoard;
    private AudioSource audioSource;
    public AudioClip clickSound;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        menuState = 0;
        startText =startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        turnCounter = GameObject.FindGameObjectWithTag("TurnCounter").GetComponent<TurnCounter>();
        animationTimer = 2.3f;
        afterPressDelay = 1.5f;
        minigameSelected = 0;
        loadMinigame = false;
        if(GameObject.FindGameObjectWithTag("SceneMark") == null)
        {
            blackPanelDown.revealOnLoad = false;
            blackPanelDown.hideUI();
        }
    }
    public void playClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
    public void ExitPress()
    {
        Application.Quit();
    }

    public void PartyPress()
    {
        if (afterPressDelay <= 0)
        {


            menuState = TURN_SELECT;
            eventSystem.SetSelectedGameObject(GameObject.Find("BackButton1"));
            menuScreens[0].revealUI();
            animationTimer = 0.5f;
            afterPressDelay = 0.3f;
        }
    }
    public void startPress()
    {
        loadBoard = true;
        afterPressDelay = 0.7f;
        blackPanelUp.revealUI();
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
            } else if (menuState == TURN_SELECT)
            {
                screenResetDelay = 0.3f;
                resetScreen = true;
                screenToReset = TURN_SELECT;
            } else if (menuState == MINIGAME_SELECT)
            {
                screenResetDelay = 0.3f;
                resetScreen = true;
                screenToReset = MINIGAME_SELECT;
                for (int i = 0; i < minigameList.Length; i++)
                {
                    if(i != minigameSelected)
                    {
                        minigameList[i].revealed = false;
                        minigameList[i].setMoving(false);
                        minigameList[i].resetToOriginalPosition();
                    }
                    
                }
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
    public void minigamePress()
    {
        if (afterPressDelay <= 0)
        {
            menuState = MINIGAME_SELECT;
            menuScreens[2].revealUI();
            eventSystem.SetSelectedGameObject(GameObject.Find("BackButton3"));
            afterPressDelay = 0.5f; //prevents button pressing while the menu screen moves
            minigameSelected = 0;
            revealMinigame = true;
            animationTimer = 0.5f;
            for(int i = 0; i < minigameList.Length; i++)
            {
                minigameList[i].resetToOriginalPosition();
            }
        }

    }
    public void increaseTurnCount()
    {
        audioSource.PlayOneShot(clickSound);
        if(turnCounter.getTurnCount() < 101)
        {
            turnCounter.setTurnCount(turnCounter.getTurnCount() + 1);
        }
    }

    public void decreaseTurnCount()
    {
        audioSource.PlayOneShot(clickSound);
        if (turnCounter.getTurnCount() > 2)
        {
            turnCounter.setTurnCount(turnCounter.getTurnCount() - 1);
        }

    }
    public void incrementMinigameSelection()
    {
        if (!loadMinigame) //don't allow players to change minigame in short time of transition
        {
            minigameList[minigameSelected].hideUI(1); //hide old minigame
            minigameSelected += 1;
            if (minigameSelected >= minigameList.Length)
            {
                minigameSelected = 0;
            }
            minigameList[minigameSelected].revealUI(1); //reveal new minigame
        }
        audioSource.PlayOneShot(clickSound);


    }
    public void decrementMinigameSelection()
    {
        if (!loadMinigame) //don't allow players to change minigame in short time of transition
        {
            minigameList[minigameSelected].hideUI(-1); //hide old minigame
            minigameSelected -= 1;
            if (minigameSelected < 0)
            {
                minigameSelected = minigameList.Length - 1;
            }
            minigameList[minigameSelected].revealUI(-1); //reveal new minigame
        }
        audioSource.PlayOneShot(clickSound);


    }
    public void BButtonPress()
    {     
        backPress();
    }
    public void startMinigame()
    {
        if (!loadMinigame)
        {
            loadMinigame = true;
            afterPressDelay = 0.7f;
            blackPanelDown.revealUI();
        }
        audioSource.PlayOneShot(clickSound);


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
        } else if(loadBoard && afterPressDelay <= 0)
        {
            SceneManager.LoadScene("gameBoard");
        } else if(loadMinigame && afterPressDelay <= 0) {
            if(minigameSelected == 0)
            {
                SceneManager.LoadScene("mini_arena");
            } else if(minigameSelected == 1)
            {
                SceneManager.LoadScene("soccerField");
            } else if(minigameSelected == 2)
            {
                SceneManager.LoadScene("hand cart");
            }

        }

        if (menuState == MAIN_MENU)
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
                
                for (int i = 0; i < turnSelectElements.Length; i++)
                {
                    turnSelectElements[i].revealUI();
                }
                if (anyPressB())
                {
                    BButtonPress();
                }
            }
        }
        else if(menuState == MINIGAME_SELECT)
        {
            if (animationTimer > 0)
            {
                animationTimer-=Time.deltaTime;

            }
            else
            {

                for (int i = 0; i < minigameElements.Length; i++)
                {
                    minigameElements[i].revealUI();
                }
                if (anyPressB())
                {
                    BButtonPress();
                }
            }
               
            
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
            if (anyPressB())
            {
                BButtonPress();
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
        } else if(screenNum == MINIGAME_SELECT)
        {
            for (int i = 0; i < minigameElements.Length; i++)
            {
                minigameElements[i].hideUI();
            }
            for(int i = 0; i < minigameList.Length; i++)
            {
                minigameList[i].revealed = false;
                minigameList[i].setMoving(false);
                minigameList[i].resetToOriginalPosition();
            }
            minigameList[0].revealUI(1);
        }
    }
    public bool anyPressB()
    {
        bool result = false;
        if (Input.GetButtonDown("P1_Fire2"))
        {
            result = true;
        } else if (Input.GetButtonDown("P2_Fire2"))
        {
            result = true;
        }
        else if (Input.GetButtonDown("P3_Fire2"))
        {
            result = true;
        }
        else if (Input.GetButtonDown("P4_Fire2"))
        {
            result = true;
        }
        return result;
    }
}
