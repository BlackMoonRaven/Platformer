using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //private Transform _target; // Змінна для визначення за яким об'єктом камера буде слідувати
    [SerializeField] private Player _player;

    private void Start()
    {
        //_target = GameObject.FindGameObjectWithTag("Player").transform; // Передаємо дані об'єкта із тегом Player
    }
    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10f); //Переміщення за гравцем
    }
}
