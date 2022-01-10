using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public GameManager gameManager;
    public float timer = 0;
    public float endTime = 0;
    public float graceTime = 2f;
    bool finishLine = false;
    TimeSpan ts = new TimeSpan();

    public Text text;
    private void Update()
    {  
        timer += Time.deltaTime;            //Pistetään kello juoksemaan pelin alussa.
        if (finishLine)                     //Jos pelaaja pääsee kentän loppuun:
        {
            Time.timeScale = 0.7f;          //Hidastetaan aikaa
            graceTime -= Time.deltaTime;    //Lasketaan aikaa alaspäin, kunnes 0
            if (graceTime <= 0)
            {
                gameManager.GameWon();      //Näytetään voitit ruudun ja pysäytetään aika
                Time.timeScale = 0f;
            }
        }
    }

    //Tarkastetaan että pelaaja on ylittänyt maaliviivan
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            finishLine = true;
            endTime = timer;                        //Otetaan juoksevasta kellosta lopullinen aika ja pysäytetään juokseva kello
            timer = 0;
            ts = TimeSpan.FromSeconds(endTime);     

            text.text = ts.ToString("mm\\:ss\\.f");     //Formatoidaan aika selkeäksi min:sec:ms ja näytetään se loppuruudussa
        }
    }
}
