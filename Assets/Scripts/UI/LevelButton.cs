using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button[] _levels ;
    private int _levelComplete;

    private void Start()
    {
        // Отримуємо кількість відкритих рівнів
        _levelComplete = PlayerPrefs.GetInt("LevelComplete");

        // По замовчуванню, всі рівні недоступні, окрім 1
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].interactable = false;
        }

        // Відносно того, скільки рівнів гравець пройшов, ми відкриваємо нові рівні
        for (int i = 0; i < _levelComplete; i++)
        {
            _levels[i].interactable = true;
        }
    }

    public void LoadLevel(int _level)
    {
        SceneManager.LoadScene(_level);
        AudioManager.instance.InitLevelAudio(_level);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void Resettttttt()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].interactable = false;
        }

        PlayerPrefs.DeleteAll();
    }
}
