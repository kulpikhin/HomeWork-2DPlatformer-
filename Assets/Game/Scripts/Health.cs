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

    public void GetDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthChanged?.Invoke(CurrentHealth);
    }

    public void Heal(int healPower)
    {
        if(CurrentHealth + healPower >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth += healPower;            
        }

        HealthChanged?.Invoke(CurrentHealth);
    }
}
