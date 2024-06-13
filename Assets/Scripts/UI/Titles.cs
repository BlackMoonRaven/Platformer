using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titles : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TheEnd());
    }

    private IEnumerator TheEnd()
    {
        yield return new WaitForSeconds(21);
        SceneManager.LoadScene(0);
    }
}
