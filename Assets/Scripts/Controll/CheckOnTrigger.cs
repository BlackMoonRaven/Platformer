using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckOnTrigger : MonoBehaviour
{
    private UnityAction onTrigger; // Це делегат Unnity для підписок на події і виклику методів

    // Метод для підписки на подію
    public void Init(UnityAction OnTrigger)
    {
        onTrigger += OnTrigger;
    }

    // Відслідковування колізії
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTrigger?.Invoke(); // Викликаємо відповідний метод який ми помістили у LevelControll
                                 // Знак "?" виконує перевірку на null, тобто перевіряє чи onTrigger пустий
        }
    }
}
