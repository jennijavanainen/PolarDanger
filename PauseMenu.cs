using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))       //Jos pelaaja painaa ESC nappulaa, pysäytetään peli.
        {
            if (GameIsPaused)                       //Jos peli on pysäytettynä ja pelaaja painaa ESC, palataan peliin
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //Palataan peliin
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //Pysäytetään peli
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;                //Pysäytetään pelin sisäinen kello, niin että mitään ei tapahdu pelissä
        GameIsPaused = true;
    }


    //Sammutetaan peli
    public void QuitGame()
    {
        Debug.Log("Peli sammuu...");
        Application.Quit();
    }

    //Palataan Start menuun
    public void Menu()
    {
        Time.timeScale = 1f;
        Debug.Log("Avataan menu...");
        SceneManager.LoadScene("Start Menu");
    }

    //Käynnistetään nykyinen kenttä uudestaan
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
