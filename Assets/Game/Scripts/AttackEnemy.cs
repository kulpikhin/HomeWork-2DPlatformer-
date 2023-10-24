using System.Collections;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosion;

    private int _damage;
    private float _attackSpeed;
    private bool _isRecharge;
    private bool _CanAttack;
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
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            _target = player;
            _CanAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            _target = null;
            _CanAttack = false;
        }
    }

    private void Attack()
    {
        if (_CanAttack && _isRecharge == false)
        {
            _target.GetDamage(_damage);
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
