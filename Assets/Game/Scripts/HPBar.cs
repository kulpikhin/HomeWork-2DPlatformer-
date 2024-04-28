using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HPBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TMP_Text _hpText;

    private Slider _hpBar;

    private int _maxHealth;

    private void Awake()
    {
        _hpBar = GetComponent<Slider>();
    }
    private void Start()
    {
        _maxHealth = _health.MaxHealth;
        _hpBar.maxValue = _maxHealth;
        ChangeHPBar(_maxHealth);        
    }

    private void OnEnable()
    {
        _health.HealthChanged += ChangeHPBar;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= ChangeHPBar;
    }

    private void ChangeHPBar(int currentHealth)
    {
        _hpText.text = currentHealth + "/" + _maxHealth;
        _hpBar.value = currentHealth;
    }
}
