using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public float floatStrength = 25f;       // Leijailunopeus
    private Rigidbody2D rb;

    private bool playerCollision = false;   // Tarkistetaanko onko pelaaja ilmapallon kohdalla

    public GameObject explosionEffect;      // Räjähdysefekti

    private Vector3 originalPosition;
    public GameObject player;

    private new SpriteRenderer renderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        originalPosition = transform.position;

        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerCollision)
        {
            if (Input.GetKeyDown("e"))
            {
                fly();
            }

            if (Input.GetKeyDown("space"))
            {
                detachPlayer();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                                         // Jos pelaaja on lähellä, ilmapallo voi liikkua
        {
            playerCollision = true;
        }

        if (collision.CompareTag("Ground"))                                         // Törmäys jääpuikkoihin/seinään
        {
            detachPlayer();

            Instantiate(explosionEffect, transform.position, transform.rotation);   // Räjähdysefekti

            AudioManager.instance.Play("explosionSFX");                             // Räjähdysääni

            Respawn();                  

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                         // Jos pelaaja poistuu ilmapallon läheltä, ilmapalloa ei voi liikuttaa
        {
            playerCollision = false;
        }

    }
    /// <summary>
    /// Siirretään ilmapallo takaisin lähtöpisteeseen
    /// </summary>
    void Respawn()
    {
        transform.position = originalPosition;
        rb.Sleep();
    }

    /// <summary>
    /// Ilmapallo leijuu ylöspäin pelaaja mukanaan
    /// </summary>
    void fly()
    {
        rb.AddForce(Vector2.up * floatStrength);                     // Ilmapallo lähtee liikkeelle
        renderer.enabled = false;                                    // Poistetaan ilmapallo näkyvistä
        player.transform.parent = transform;                         // Otetaan pelaaja mukaan
        player.GetComponent<Rigidbody2D>().isKinematic = true;

        player.GetComponent<Animator>().SetBool("isFlying", true);   // Pelaajan ilmapallo-animaatio
        AudioManager.instance.Play("surpriseSFX");
    }

    /// <summary>
    /// Irrotetaan pelaaja ilmapallosta
    /// </summary>
    void detachPlayer()
    {
        renderer.enabled = true;
        player.transform.parent = null;                                 // Irrotetaan pelaaja
        player.GetComponent<Animator>().SetBool("isFlying", false);     // Animaatio loppuu                           
        player.GetComponent<Rigidbody2D>().isKinematic = false;         
    }
}
