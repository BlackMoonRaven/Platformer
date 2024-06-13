using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool _active = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Continue()
    {
        gameObject.SetActive(false);
        _active = false;
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        AudioManager.instance.InitLevelAudio(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Active()
    {
        _active = !_active;
        if (_active)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        gameObject.SetActive(_active);
    }
}
