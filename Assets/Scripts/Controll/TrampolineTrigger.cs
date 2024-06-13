using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineTrigger : MonoBehaviour
{
    [SerializeField] private float _jumpPower;
    private Rigidbody2D _rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _rb = collision.GetComponent<Rigidbody2D>();
            // Відбувається підкидування гравця вгору і вправо
            _rb.AddForce((transform.up + transform.right) * _jumpPower, ForceMode2D.Impulse);
        }
    }
}
