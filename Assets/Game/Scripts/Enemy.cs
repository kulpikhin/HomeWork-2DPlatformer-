using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _maxHealth;
    private int _currentHealth;

    private void Awake()
    {
        _maxHealth = 100;
        _currentHealth = _maxHealth;
    }
    
    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    } 

}
