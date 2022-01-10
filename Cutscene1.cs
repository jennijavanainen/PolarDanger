using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscene1 : MonoBehaviour
{
    public GameObject timelineManager;
    public Image blackoutImage;
    private Color blackoutColor;
    public float lerpMultiplier = 0.02f;

    public GameObject controls;
    public GameObject camera2;

    private void Start()
    {
        blackoutColor = new Color(1, 1, 1, 0);     
        blackoutImage.color = blackoutColor;
    }

    // Update is called once per frame
    void Update()
    {

        if (timelineManager.GetComponent<PlayableDirector>().state != PlayState.Playing)
        {
            camera2.SetActive(true);

            blackoutColor = Color.Lerp(blackoutColor, new Color(1, 1, 1, 1), Time.time * lerpMultiplier);       //  Ruutu pimenee vähitellen
            blackoutImage.color = blackoutColor;

            controls.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Level");
            }
        }

    }

    
}
