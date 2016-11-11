using UnityEngine;
using System.Collections;

public class finishLineScript : MonoBehaviour
{
    void OnTriggerEnter(Collider col )
    {
        if (col.tag == "player")
        {
            //Do something like finishing the game or changing the scene
            print("finish");
        }
    }
}
