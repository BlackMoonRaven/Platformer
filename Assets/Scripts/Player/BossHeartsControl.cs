using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHeartsControl : MonoBehaviour
{
    [SerializeField] private Image[] _heatrs;
    [SerializeField] private Sprite _spriteZeroHeart;

    public void LosseHealth(int heart) => _heatrs[heart].sprite = _spriteZeroHeart;
}
