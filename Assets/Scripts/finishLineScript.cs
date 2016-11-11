using UnityEngine;
using System.Collections;


public class finishLineScript : MonoBehaviour
{
    private ArrayList placement = new ArrayList(); 

    void OnTriggerEnter(Collider col )
    {
        //Do something like finishing the game or changing the scene
            col.tag = "Point";
            print("finish" + " " +col.name);
            placement.Add(col);
      

        
    }

    void Update()
    {
        if(placement.Count==4)
        {
            print("game done");
        }
    }

}
