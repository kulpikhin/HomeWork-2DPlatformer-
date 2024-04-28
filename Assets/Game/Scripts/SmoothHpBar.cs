using System.Collections;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Slider))]
public class SmoothHpBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _speedHpBar;

    private Coroutine _smoothlyChangeHPBar;
    private Slider _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
        _healthBar.maxValue = _health.MaxHealth;
        _healthBar.value = _healthBar.maxValue;
    }

    private void OnEnable()
    {
        _health.HealthChanged += StartSmoothlyChangeHP;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= StartSmoothlyChangeHP;
    }

    private void StartSmoothlyChangeHP(int curentHealth)
    {
        if (_smoothlyChangeHPBar != null)        
            StopCoroutine(_smoothlyChangeHPBar);        

        _smoothlyChangeHPBar = StartCoroutine(SmoothlyChangeHp(curentHealth));
    }

    private IEnumerator SmoothlyChangeHp(int curentHealth)
    {
        while (_healthBar.value != curentHealth)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, curentHealth, _speedHpBar);
            yield return null;
        }
    }
}
