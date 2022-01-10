using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private void step1 ()
    {
        AudioManager.instance.Play("step1");
    }

    private void step2()
    {
        AudioManager.instance.Play("step2");
    }

    private void disco()        // when you're dead as disco
    {
        AudioManager.instance.Play("drowningSFX");
    }



}
