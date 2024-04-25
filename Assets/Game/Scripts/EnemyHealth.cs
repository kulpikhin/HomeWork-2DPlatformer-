using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider _hpSmoothlyBar;

    private Coroutine _smoothlyChangeHPBar;

    private int _maxHealth;
    private int _currentHealth;
    private float _speedHpBar = 1f;

    private void Awake()
    {
        _maxHealth = 100;
        _currentHealth = _maxHealth;
        _hpSmoothlyBar.maxValue = _maxHealth;
        _hpSmoothlyBar.value = _maxHealth;
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        StartSmoothlyChangeHP();
        Death();
    }

    private void Death()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
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
