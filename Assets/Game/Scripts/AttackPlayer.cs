using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private InputSystem _inputSystem;

    private PlayerMover _controller;

    private void Awake()
    {
        _controller = GetComponentInParent<PlayerMover>();
    }

    private void OnEnable()
    {
        _inputSystem.ShootKeyGeted += Attack;
    }

    private void OnDisable()
    {
        _inputSystem.ShootKeyGeted -= Attack;
    }

    private void Attack()
    {
        Bullet bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        bullet.IsFaceRight = _controller.IsFaceRight;
    }
}
