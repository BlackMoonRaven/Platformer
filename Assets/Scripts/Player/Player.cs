using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed; // Змінна для редагування швидкості руху гравця
    [SerializeField] private float _jump; // Змінна для редагування висоти стрибка
    [SerializeField] private Transform _groundCheck; /* Змінна для взаємодії дочірнього пустого обє'кта,
                                                        який допоможе реалізувати колізію із платформами 
                                                        для реалізації справжнього стрибка
                                                     */
    [SerializeField] private LayerMask _groundLayer; // Змінна для визначення на якому лейері знаходиться платформа 
    [SerializeField] private MoveHintPage _moveHintPage;
    [SerializeField] private TMP_Text _keyCount;
    [SerializeField] private Pause _pause;


    private Rigidbody2D _rb; // Змінна для взаємодії із однойменним компонентом в об'єкті
    private SpriteRenderer _spriteRenderer; // Змінна для взаємодії із однойменним компонентом в об'єкті
    private bool _isGrounded; // Змінна для перевірки чи відбулась колізія
    private Animator _animator; // Змінна для роботи із анімаціями
    private bool _hintActive;
    private int keys = 0;

    public int Key { get { return keys; } }

    [HideInInspector] public Vector2 _position; // Змінна для встановлення нового спавну гравця коли він не зміг вперше пройти рівень 
    [HideInInspector] public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } }

    void Start() 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Отримуємо компонент SpriteRenderer цього об'єкта
        _rb = GetComponent<Rigidbody2D>(); // Отримуємо компонент Rigidbody2D цього об'єкта
        _position = new Vector2(-3.5f, -2.5f); // Задаємо початков точку спавну
        _animator = GetComponent<Animator>();
        _hintActive = false;
    }

    void Update()
    {
        // Стрибок, якщо натиснута клавіша Space
        if (Input.GetKey(KeyCode.Space))
        {
            Jump(_jump, _isGrounded);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _moveHintPage._levelHints[0].SetActive(_hintActive);
            for (int i = 1; i < _moveHintPage._levelHints.Length; i++)
            {
                _moveHintPage._levelHints[i].SetActive(false);
            }
            if (_hintActive)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            _hintActive = !_hintActive;
            _moveHintPage.Refresh();
        }

        // Пауза
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _pause.Active();
        }

        // Зміна анімації
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _animator.Play("Walk");
        }
        else if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)))
        {
            _animator.Play("Run");
        }
        else if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftControl)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftControl)))
        {
            _animator.Play("Sneaking");
            
        }
        else
        {
            _animator.Play("Idle");
        }

        _keyCount.text = $"x {keys}";
    }

    void FixedUpdate() 
    {
        /* Передача у змінну True, якщо відбулася колізія із платформами і False якщо наоборот
         У метод OverlapCapsule ми передаєм наступні значення :
         1. Вказуємо позицію дочірнього пестого об'єкта який буде взаємодіяти із землею
         2. Вказуємо величину колайдера 
         3. Вказуємо позицію колайдера (Горизонтальна в даному випадку)
         4. Вказуємо відстань яку охоплює капсула
         5. Вказуємо шар з яким повинен взіємодіяти об'єкт
        */
        _isGrounded = Physics2D.OverlapCapsule(_groundCheck.position, new Vector2(0.57f, 0.12f), CapsuleDirection2D.Horizontal, 0, _groundLayer);

        if (Input.GetKey(KeyCode.D)) // Переміщення вправо і обертання об'єкта вправо якщо натиснута клавіша D
        {
            Move(_speed, true);
        }
        else if (Input.GetKey(KeyCode.A)) // Переміщення вліво і обертання об'єкта вліво якщо натиснута клавіша A
        {
            Move(_speed, false);
        }
        else
        {
            if (_isGrounded)
            {
                _rb.velocity = new Vector2(0f, _rb.velocity.y);
            }
        }

        // Телепортація
        if (Input.GetKey(KeyCode.J))
        {
            Teleport(_position);
        }
    }

    // Метод для руху гравця в певну сторону із певною швидкістю
    private void Move(float speed, bool moveInRight)
    {
        _spriteRenderer.flipX = moveInRight;

        if (moveInRight)
        {
            _rb.velocity = new Vector2(Speed(speed), _rb.velocity.y); // Рух вправо
        }
        else
        {
            _rb.velocity = new Vector2(Speed(speed) * (-1), _rb.velocity.y); // Рух вліво
        }
    }

    // Метод для прищвидшення, сповільнення або ж сталої швидкості гравця
    private float Speed(float speed)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return speed * 2; // Пришвидшення 
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            return speed / 2; // Сповільнення
        }
        else
        {
            return speed; // Нормальна швидкість
        }
    }

    // Метод для реалізації стрибка
    private void Jump(float hightJump, bool isGrounded)
    {
        if (isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, hightJump);
            AudioManager.instance.PlayeSFX(AudioManager.instance._jump);
        }
    }

    // Метод для телепортації персонажа на початкову точку
    public void Teleport(Vector2 position)
    {
        _rb.velocity = Vector2.zero;
        transform.position = position;
    }

    // Метод за допомогою якого відбуваєтсья зміна точки респавну відносно стіни з якою стикнувся гравець
    public void ChangePosition()
    {
        if (_position.x == -3.5f)
        {
            _position = new Vector2(74f, 2f);
        }
        else
        {
            _position = new Vector2(-3.5f, -2.5f);
        }
    }

    public void AddKey()
    {
        keys++;
    }

    public void SpendKey()
    {
        keys--;
    }
}
