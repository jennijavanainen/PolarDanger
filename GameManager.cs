using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameWonUI;
    void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        AudioManager.instance.Play("bgmusic");
    }

    public void GameWon()
    {
        gameWonUI.SetActive(true);
    }

}
