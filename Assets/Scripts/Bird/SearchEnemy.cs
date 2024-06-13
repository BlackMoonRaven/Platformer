using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchEnemy : MonoBehaviour
{
    [SerializeField] private BirdControl _bird;
    [SerializeField] private Transform _boss;
    [SerializeField] private float _speed;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_bird.FindEnemy)
            {
                _bird.transform.position = Vector2.MoveTowards(_bird.transform.position, _boss.position, _speed);
            }
        }
    }
}
