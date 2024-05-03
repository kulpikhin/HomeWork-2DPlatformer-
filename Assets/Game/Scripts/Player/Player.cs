using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]

public class Player : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;

    private Animator _animator;
    private PlayerMover _playerMover;

    public bool IsAlive { get; private set; } = true;
    public Health HealthOwn { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        HealthOwn = GetComponent<Health>();
        _playerMover = GetComponent<PlayerMover>();
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
            IsAlive = false;
            _playerMover.enabled = false;
            _animator.SetBool(PlayerAnimatorData.Params.IsDeath, true);
        }
    }
}
