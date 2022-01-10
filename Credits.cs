using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public float waitTime = 18f;
    public GameObject winScreen;
    private void Update()
    {
        winScreen.SetActive(false);         //Poistetaan voitto ikkuna näkyvistä
        StartCoroutine(QuitGame());         //Aloitetaan pelin sammuttaminen
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSecondsRealtime(waitTime);      //Peli sammuu 18sec kuluttua, jolloin creditit on ehtineet mennä

        Debug.Log("Peli Sammuu..");
        Application.Quit();                                     //Sammutetaan peli
    }

}
