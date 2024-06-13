using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeartsControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Image> _hearts; // За допомогою списку отримуємо доступ до всіх сердечок
    [SerializeField] private Sprite _spriteZeroHeart; // Змінна в якій міститься спрайт незаповненого сердечка
    [SerializeField] private Sprite _spriteFullHeart; // Змінна в якій міститься спрайт заповненого сердечка
    [SerializeField] private int _scene;

    private int _helth;

    private void Start()
    {
        _helth = 5;
    }

    private void Update()
    {
        if (_helth == 0) 
        {
            Deth();
        }
    }

    // Метод для додавання та віднімання здоров'я в гравця, а також контролью
    // щоб користувач не міг вийти за рамки дозволених значень
    private void ChangeHealth(int count)
    {
        _helth += count;
        _helth = Mathf.Clamp(_helth, 0, 5);
    }

    // Метод для відновлення сердець
    public void GetHealth()
    {
        AudioManager.instance.PlayeSFX(AudioManager.instance._heal);
        ChangeHealth(1); // Додаєм 1 сердечко
        _hearts[_helth - 1].sprite = _spriteFullHeart; // Змінюєм сердечко на заповнине
    }

    public void LossHealth()
    {
        AudioManager.instance.PlayeSFX(AudioManager.instance._takeDamage);
        ChangeHealth(-1); // У методі ChangeHealth() використовується тільки додавання, тому щоб забрати здоров'я використовуємо значення -1
        _hearts[_helth].sprite = _spriteZeroHeart; // Заміняєм сердечко на пусте
        StartCoroutine(ChangeColor());
    }

    private void Deth()
    {
        SceneManager.LoadScene(_scene);
    }

    private IEnumerator ChangeColor()
    {
        _player.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
