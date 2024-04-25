using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Controller))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject _playerSprite;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _hpSmoothlyBar;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private float _speedHpBar;

    private int _maxHealth;
    private int _currentHealth;

    private Coroutine _smoothlyChangeHPBar;
    private Controller _controller;
    private Animator _animator;
    private bool _isAlive;

    public bool IsAlive { get => _isAlive; }

    private void Awake()
    {
        _isAlive = true;
        _animator = _playerSprite.GetComponent<Animator>();
        _controller = GetComponent<Controller>();
        _maxHealth = 100;
        _currentHealth = _maxHealth;
        _hpBar.maxValue = _maxHealth;
        _hpSmoothlyBar.maxValue = _maxHealth;
        _hpSmoothlyBar.value = _maxHealth;
        ChangeHPInterface();
    }

    public void Heal(int healingPower)
    {
        _currentHealth += healingPower;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        ChangeHPInterface();
    }

    private void ChangeHPInterface()
    {
        ChangeHPBar();
        ChangeHpText();
        StartSmoothlyChangeHP();
    }

    private void ChangeHpText()
    {
        _hpText.text = _currentHealth + "/" + _maxHealth;
    }

    private void ChangeHPBar()
    {
        _hpBar.value = _currentHealth;
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        ChangeHPInterface();
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

    private void StartSmoothlyChangeHP()
    {
        if (_smoothlyChangeHPBar != null)
        {
            StopCoroutine(_smoothlyChangeHPBar);
        }

        _smoothlyChangeHPBar = StartCoroutine(SmoothlyChangeHp());
    }

    private IEnumerator SmoothlyChangeHp()
    {
        while (_hpSmoothlyBar.value != _currentHealth)
        {
            _hpSmoothlyBar.value = Mathf.MoveTowards(_hpSmoothlyBar.value, _currentHealth, _speedHpBar);
            yield return null;
        }
    }
}
