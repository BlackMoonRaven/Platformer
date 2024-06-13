using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
        AudioManager.instance.InitLevelAudio(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
