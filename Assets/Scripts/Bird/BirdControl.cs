using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    [SerializeField] private Sprite _spriteBird;
    [SerializeField] private BossControl _boss;

    private bool _findEnemy = false;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public bool FindEnemy { get { return _findEnemy; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<Player>().Key > 0)
        {
            _findEnemy = true;
            _animator.Play("Fly");
            collision.GetComponent<Player>().SpendKey();
            gameObject.GetComponent<SpriteRenderer>().sprite = _spriteBird;
            AudioManager.instance.PlayeSFX(AudioManager.instance._birdFly);
        }

        if (collision.CompareTag("BossMain") && gameObject.GetComponent<SpriteRenderer>().sprite == _spriteBird)
        {
            _boss.TakeDamage();
            AudioManager.instance.PlayeSFX(AudioManager.instance._bossTakeDamage);
            Destroy(gameObject);
        }
    }

}
