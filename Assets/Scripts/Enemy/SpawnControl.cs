using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab; // Посилання на прифаб ворога

    private GameObject _newEnemy; // Новий дочірній об'єкт (ворог)
    private Transform _leftSide; // Крайня ліва точка для руху ворога
    private Transform _rightSide; // крайня права точка для руху ворога
    private bool _isSpawn; // Змінна яка вказує на те, чи можна заспавнити нового

    public Transform LeftSide { get { return _leftSide; } } // Властивість для передання координат лівої точки ворогу 
    public Transform RightSide { get { return _rightSide; } } // Властиввість для передання правої точки координат ворогу

    private void Start()
    {
        _leftSide = transform.Find("Left");
        _rightSide = transform.Find("Right");

        StartCoroutine(Spawn());
    }

    private void Deth()
    {
        Destroy(_newEnemy);

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));
        _newEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        _newEnemy.transform.parent = transform;
        _newEnemy.transform.GetChild(0).GetComponent<DethEnemyComtrol>().Init(Deth);
    }
}
