using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameInfo : MonoBehaviour
{
    [SerializeField] private GameObject _canvas; // Змінна для взаємодії із об'єктами що знаходяться в Canvas
    [SerializeField] private TextMeshProUGUI _result; // Змінна для взаємодії із текстом
    

    void Start()
    {
        _canvas.SetActive(false); // Робимо Canvas неактивним
    }

    // Метод для виводу кількості кількості спроб, щоб завершитти гру
    public void PrintText(int counter)
    {
        _result.text = $"Ти зміг пройти гру з {counter}-го разу";
    }

    // Метод який робить Canvas активним
    public void ActiveCanvas()
    {
        _canvas.SetActive(true);
    }
}
