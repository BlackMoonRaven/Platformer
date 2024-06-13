using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] private int _currentScene;

    // Метод для перезагрузки рівня при натисканні на кнопку
    public void RefreshGame()
    {
        LevelController.Instance.IsEndGame(); 
        SceneManager.LoadScene(_currentScene);
        AudioManager.instance.InitLevelAudio(_currentScene);
    }
}
