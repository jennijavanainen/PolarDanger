using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour
{
    public float gracePeriod = 7f;          //Aika, mikä kestää ennen kuin vesi alkaa nousemaan
    public GameObject Water;
    public GameObject Died;
    public float risingSpeed = 1f;          //Veden nousun nopeus

    float waitTime = 7f;                    //Aika mikä odotetaan jos pelaaja on vedessä, kunnes pysäytetään vedennouseminen
    private void Update()
    {
        gracePeriod -= Time.deltaTime;

        if(gracePeriod < 0)                 
        {
            gracePeriod = 0;

            Water.transform.Translate(new Vector3(0, risingSpeed, 0));      //Vesi alkaa nousemaan
            if (PauseMenu.GameIsPaused)
            {
                Water.transform.Translate(new Vector3(0, 0, 0));            //Jos peli pysäytetään (Pausetetaan), pysäytetään veden nouseminen
            }
            if (PlayerMovement1.drowning)                                   //Tarkastetaan onko pelaaja hukkumassa
            {
                waitTime -= Time.deltaTime;
                if (waitTime <= 0)                                          //Kun odotusaika on mennyt, pysäytetään veden nouseminen ja pelaajan liikkuminen
                {
                    Time.timeScale = 0f;
                    Water.transform.Translate(new Vector3(0, 0, 0));

                }
            }
            
        }
    }
}
