using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    private int _sceneIndex;
    private int _levelComplete;

    private void Awake()
    {
        if (LevelController.Instance == null)
        {
            Instance = this; 

            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        _levelComplete = PlayerPrefs.GetInt("LevelComplete");    
    }


    public void IsEndGame()
    {
        if (_sceneIndex >= _levelComplete)
        {
            PlayerPrefs.SetInt("LevelComplete", _sceneIndex);
        }
    }
}
