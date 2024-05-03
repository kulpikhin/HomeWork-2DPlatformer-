using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HPBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    protected Slider _hpBar;
    protected int _maxHealth;

    private void Awake()
    {
        _hpBar = GetComponent<Slider>();
    }
    private void Start()
    {
        _maxHealth = _health.MaxHealth;
        _hpBar.maxValue = _maxHealth;
        _hpBar.value = _maxHealth;
    }

    private void OnEnable()
    {
        _health.HealthChanged += ChangeHPBar;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= ChangeHPBar;
    }

    protected virtual void ChangeHPBar(int currentHealth)
    {
        _hpBar.value = currentHealth;
    }
}
