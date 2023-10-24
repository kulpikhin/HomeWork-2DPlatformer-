using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    private Controller _controller;

    private void Awake()
    {
        _controller = GetComponentInParent<Controller>();        
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Bullet bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.IsFaceRight = _controller.IsFaceRight;
        }
    }
}
