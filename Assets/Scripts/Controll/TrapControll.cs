using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TrapControll : MonoBehaviour
{
    [SerializeField] private HeartsControl _heartsControl; // Змінна для взіємодії із здоров'ям гравця

    private void Start()
    {
        // Передаємо можливість віднімати здоров'я усім шипам на карті (підписуєм їх на подію)
        foreach (Transform t in transform)
        {
            if (t.TryGetComponent<CheckOnTrigger>(out CheckOnTrigger component))
            {
                if (t.CompareTag("Trap"))
                {
                    component.Init(_heartsControl.LossHealth);
                }
            }
        }
    }
}
