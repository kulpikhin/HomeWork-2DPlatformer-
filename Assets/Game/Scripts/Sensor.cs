using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sensor : MonoBehaviour
{
    private EnemyActions _enemy;
    private Player _target;

    private void Awake()
    {
        _enemy = gameObject.GetComponentInParent<EnemyActions>();
    }

    private void Update()
    {
        CheckTarget();
    }

    private void CheckTarget()
    {
        if (_target != null)
        {
            if (_target.IsAlive)
            {
                _enemy.PlayerTarget = _target.transform;
            }
            else
            {
                _enemy.PlayerTarget = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player) && player.IsAlive)
        {
            _target = player;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {            
            _target = null;
            _enemy.PlayerTarget = null;
        }
    }
}
