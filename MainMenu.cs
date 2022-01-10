using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("bgmusic");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       //Avaa seuraavan scenen scene indexin mukaan
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();                                                        //Sammuttaa pelin
    }
}
