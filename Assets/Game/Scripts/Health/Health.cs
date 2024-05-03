using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    public event UnityAction<int> HealthChanged;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (damage > 0)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, _maxHealth);

            HealthChanged?.Invoke(CurrentHealth);
        }
    }

    public void Heal(int healPower)
    {
        if (healPower > 0)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + healPower, 0, _maxHealth);

            HealthChanged?.Invoke(CurrentHealth);
        }        
    }
}
