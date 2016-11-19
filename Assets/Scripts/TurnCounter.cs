using UnityEngine;
using System.Collections;

public class TurnCounter : MonoBehaviour {

    public int turnCount;
    void Awake()
    {
        
    }
    public void setTurnCount(int x)
    {
        turnCount = x;
    }
    public int getTurnCount()
    {
        return turnCount;
    }
    public bool gameIsOver()
    {
        return turnCount <= 0;
    }
    public void decrementTurnCount()
    {
        turnCount -= 1;
    }
}
