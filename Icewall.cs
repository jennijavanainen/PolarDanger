using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Icewall : MonoBehaviour
{
    public Tilemap icewall;

    public GameObject explosionEffect;
    private bool playerNear = false;
    GameObject player;


    void Start()
    {
        icewall = GetComponent<Tilemap>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if(playerNear)
        {
            if(Input.GetKeyDown("q"))
            {
                Destroy();
            }
        }
    }

    //Tarkitetaan onko pelaaja lähellä
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

    //Tuhotaan jääseinä
    public void Destroy()
    {

        Vector3 hitPosition = player.transform.position + new Vector3(-0.8f, 0f, 0f);
        icewall.SetTile(icewall.WorldToCell(hitPosition), null);
        Instantiate(explosionEffect, hitPosition, transform.rotation);
        hitPosition = player.transform.position + new Vector3(-0.8f, 1f, 0f);
        icewall.SetTile(icewall.WorldToCell(hitPosition), null);
        Instantiate(explosionEffect, hitPosition, transform.rotation);
        AudioManager.instance.Play("iceSFX");
    }

 
}
