using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private float _speed;

    bool _turning;
    private Vector3 _pos;
    private SpriteRenderer _spriteRenderer;
    private SpawnControl _spawnControl;
    private bool _isMove = true;

    void Start()
    {
        _spawnControl = GetComponentInParent<SpawnControl>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _turning = false;
        _pos = _spawnControl.LeftSide.position;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_isMove)
        {
            _spriteRenderer.flipX = _turning;

            if (transform.position == _spawnControl.LeftSide.position)
            {
                _turning = true;
                _pos = _spawnControl.RightSide.position;
            }
            if (transform.position == _spawnControl.RightSide.position)
            {
                _turning = false;
                _pos = _spawnControl.LeftSide.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, _pos, _speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StopMove());
        }
    }

    private IEnumerator StopMove()
    {
        _isMove = false;
        yield return new WaitForSeconds(2);
        _isMove = true;
    }
}
