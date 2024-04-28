using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public Health HealthOwn { get; private set; }

    private void Awake()
    {
        HealthOwn = GetComponent<Health>();
    }

    private void OnEnable()
    {
        HealthOwn.HealthChanged += Death;
    }

    private void OnDisable()
    {
        HealthOwn.HealthChanged -= Death;
    }

    private void Death(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
