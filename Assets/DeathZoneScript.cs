using UnityEngine;
using System.Collections;

public class DeathZoneScript : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        print("hit");
        other.gameObject.SetActive(false);
    }
} 



