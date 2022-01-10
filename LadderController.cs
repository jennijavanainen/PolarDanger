using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    public float climbSpeed = 0.3f;
    float my;
    void Update()
    {
        my = Input.GetAxisRaw("Vertical");
    }

    //Katsotaan onko pelaaja tikapuilla
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {   
            collider.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.489f+my*climbSpeed);   //Jos pelaaja on tikapuilla, annetaan hänelle kiipeämisvoimaa, 
                                                                                                    //niin että painovoima ei häneen silloin vaikuta
        }
    }
}
