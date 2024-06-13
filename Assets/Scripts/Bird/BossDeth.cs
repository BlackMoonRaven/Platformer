using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeth : MonoBehaviour
{
    [SerializeField] private BossControl _boss;
    
    void Update()
    {
        //if(GameObject.Find("Boss") == null)
        //{
        //    SceneManager.LoadScene(4);
        //}

        if(gameObject.transform.childCount == 0)
        {
            _boss.Deth();
            SceneManager.LoadScene(4);
        }
    }
}
