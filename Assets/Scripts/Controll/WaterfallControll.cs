using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterfallControll : MonoBehaviour
{
    [SerializeField] private HeartsControl _heartsControl;

    private Coroutine _coroutine; // Змінна для зберігання посилання на корутину

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _coroutine = StartCoroutine(heal()); // Викликаємо корутину, та зберігаємо посилання на неї
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine(_coroutine); // Зупинка корутини
        }
        
    }

    private IEnumerator heal()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // Зупиняєм час на 1с
            _heartsControl.GetHealth(); // Викликаєм метод відновлення здоров'я
        }
    }

}
