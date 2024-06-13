using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    [SerializeField] private HeartsControl _heartsControl;
    [SerializeField] private SearchControl _searchControl;
    [SerializeField] private BossHeartsControl _bossHearts;

    private int _health = 8;

    private void Update()
    {
        if (_health == 0)
        {
            AudioManager.instance.PlayeSFX(AudioManager.instance._victory);
            Deth();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(DealDamage());
    }

    private IEnumerator DealDamage()
    {
        
        _heartsControl.LossHealth();
        _searchControl.StopAndContinueMove();
        yield return new WaitForSeconds(3);
        _searchControl.StopAndContinueMove();
        
    }

    public void TakeDamage()
    {
        _health--;
        _bossHearts.LosseHealth(_health);
    }

    public void Deth()
    {
        Destroy(gameObject);
    }
}
