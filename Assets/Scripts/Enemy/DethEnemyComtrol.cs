using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DethEnemyComtrol : MonoBehaviour
{
    private UnityAction onDeath;

    [SerializeField] private GameObject _key;

    public void Init(UnityAction OnDeath)
    {
        onDeath += OnDeath;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayeSFX(AudioManager.instance._enemyDeth);
            if (Random.Range(1, 5) == 1)
            {
                Instantiate(_key, transform.parent.position, Quaternion.identity);
            }

            onDeath?.Invoke();
        }
    }
}
