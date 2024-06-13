using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHintPage : MonoBehaviour
{
    public GameObject[] _levelHints;

    private int _curentHints = 0;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void NextPage()
    {
        _levelHints[_curentHints].SetActive(false);
        _curentHints++;
        _levelHints[_curentHints].SetActive(true);
    }

    public void PreviousPage() 
    {
        _levelHints[_curentHints].SetActive(false);
        _curentHints--;
        _levelHints[_curentHints].SetActive(true);
    }

    public void Refresh()
    {
        _curentHints = 0;
    }
}
