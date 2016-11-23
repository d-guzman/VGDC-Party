using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WinnerList : MonoBehaviour {

    // Use this for initialization
    List<int> winners;
	void Start () {
        winners = new List<int>();
	}
	
	public List<int> getWinners()
    {
        return winners;
    }
    public void setWinners(List<int> x)
    {
        winners = x;
    }
}
