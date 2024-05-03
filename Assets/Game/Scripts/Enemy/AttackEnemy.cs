using System.Collections;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosion;

    private bool _isRecharge;
    private bool _canAttack;

    private int _damage;
    private float _attackSpeed;
    private WaitForSeconds _waitTime;
    private Player _target;

    private void Awake()
    {
        _damage = 20;
        _attackSpeed = 1f;
        _waitTime = new WaitForSeconds(_attackSpeed);
    }

    private void Update()
    {        
        Attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _target = player;
            _canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _target = null;
            _canAttack = false;
        }
    }

    private void Attack()
    {
        if (_canAttack && _isRecharge == false)
        {
            _target.HealthOwn.DealDamage(_damage);
            _explosion.Play();
            StartCoroutine(Recharge());
        }            
    }

    private IEnumerator Recharge()
    {
        _isRecharge = true;
        yield return _waitTime;
        _isRecharge = false;
    }
}
