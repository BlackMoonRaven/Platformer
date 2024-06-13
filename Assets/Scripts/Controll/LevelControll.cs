using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControll : MonoBehaviour
{
    [SerializeField] private GameObject _startFlag; // Змінна для взаємодії із прапором на початку рівня
    [SerializeField] private GameObject _endFlag; // Змінна для взаємодії із прапором в кінці рівня
    [SerializeField] private GameObject _startWall; // Змінна для взаємодії із невидимою стіною (трігером) на початку рівня
    [SerializeField] private GameObject _endWall; // Змінна для взаємодії із невидимою стіною (трігером) в кінці рівня
    [SerializeField] private EndGameInfo _endGameInfo; // Змінна для взаємодії із UI елементом
    [SerializeField] private Player _player; // Змінна для відслідковування позиції гравця
    private bool _active; // Змінна для активації прапорців та стін
    private int _counter = 1; // Змінна для відслідковування за яким разом гравець пройшов гру

    void Start()
    {
        //Робимо прапор і стіну (трігер) на початку рівня неактивними
        _startFlag.SetActive(false);
        _startWall.SetActive(false);
        _active = true;

        // За допомогою foreach в нас відбувається підписка на подію (в нашому випадку це колізія із різними елементами)
        // Ми також перевіряєм якому об'єкту (за допомогою тегу) надається певний метод, коли відбудеться колізія із ним
        foreach (Transform t in transform)
        {
            if (t.TryGetComponent<CheckOnTrigger>(out CheckOnTrigger component))
            {
                if (t.CompareTag("Wall"))
                {
                    component.Init(OnWallTrigger);
                }
                if (t.CompareTag("Flag"))
                {
                    component.Init(OnFlagTrigger);
                }
                if (t.CompareTag("Abyss"))
                {
                    component.Init(OnAbbysTrigger);
                }
            } 
        }
    }

    // Метод для колізії із невидимою стіною
    private void OnWallTrigger()
    {
        ActiveFlagAndWall(_active);

        if (_active)
        {
            _active = false;
            _counter++;
            _player.ChangePosition(); // Викликаємо метод точки для повернення гравця (при падінні/телепортації)
        }
        else
        {
            _active = true;
            _counter++;
            _player.ChangePosition();
        }
    }

    // Метод для колізії із прапорцем
    private void OnFlagTrigger()
    {
        StartCoroutine(CompliteLevel());
    }

    // Метод для колізії із платформою, коли гравець падає
    private void OnAbbysTrigger()
    {
        StartCoroutine(LoseLevel());
    }

    // Метод для зміни активних прапорів та стін
    private void ActiveFlagAndWall(bool startActive)
    {
        if (startActive)
        {
            _endFlag.SetActive(false);
            _endWall.SetActive(false);
            _startFlag.SetActive(true);
            _startWall.SetActive(true);
        }
        else
        {
            _endFlag.SetActive(true);
            _endWall.SetActive(true);
            _startFlag.SetActive(false);
            _startWall.SetActive(false);
        }
    }

    private IEnumerator CompliteLevel()
    {
        AudioManager.instance.PlayeSFX(AudioManager.instance._complate);
        yield return new WaitForSeconds(4);
        _endGameInfo.PrintText(_counter); // Встановлюємо кількість спроб пройти рівень
        _endGameInfo.ActiveCanvas(); // Виводим текст та даємо гравцю можливість перепройти рівень (за бажанням)
    }

    private IEnumerator LoseLevel()
    {
        AudioManager.instance.PlayeSFX(AudioManager.instance._deth);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
