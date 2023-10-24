using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _playerSprite;

    private Animator _animator;
    private int _maxHealth;
    private int _currentHealth;
    private Controller _controller;
    private bool _isAlive;

    public bool IsAlive { get => _isAlive; }

    private void Awake()
    {
        _isAlive = true;
        _animator = _playerSprite.GetComponent<Animator>();
        _controller = GetComponent<Controller>();
        _maxHealth = 100;
        _currentHealth = _maxHealth;
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        Death();
    }

    private void Death()
    {
        if (_currentHealth <= 0)
        {
            _isAlive = false;
            _controller.enabled = false;
            _animator.SetBool(PlayerAnimatorData.Params.IsDeath, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            TakeCoin(coin);
        }
        else if(collision.gameObject.TryGetComponent<Apple>(out Apple apple))
        {
            TakeApple(apple);
        }
    }

    private void TakeCoin(Coin coin)
    {
        coin.BecomeRecived();
    }

    private void TakeApple(Apple apple)
    {
        Heal(apple.HealingPower);
        apple.BecomeRecived();
    }

    private void Heal(int healingPower)
    {
        _currentHealth += healingPower;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}
