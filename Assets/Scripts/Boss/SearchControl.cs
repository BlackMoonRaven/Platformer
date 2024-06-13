using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _npcPosition;
    [SerializeField] private float _speed;

    private bool _isMove = true;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.CompareTag("Boss"))
            {
                if (_isMove)
                {
                    _npcPosition.position = Vector2.MoveTowards(_npcPosition.position, _player.transform.position, _speed);
                }
            }
            if (transform.CompareTag("Companion"))
            {
                if (_player.SpriteRenderer.flipX)
                {
                    _npcPosition.position = Vector2.MoveTowards(_npcPosition.position, new Vector2(_player.transform.position.x + 1, _player.transform.position.y + 2), _speed);
                }
                else
                {
                    _npcPosition.position = Vector2.MoveTowards(_npcPosition.position, new Vector2(_player.transform.position.x - 1, _player.transform.position.y + 2), _speed);
                }

            }
        }
    }

    public void StopAndContinueMove() => _isMove = !_isMove;
}