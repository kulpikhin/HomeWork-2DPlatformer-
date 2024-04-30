using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private int _tickDrainAmount;

    private float _durationCount = 6f;
    private float _tickCount = 0.1f;

    private Coroutine _drainCoroutine;
    private Coroutine _durationCoroutine;

    private WaitForSeconds _waitTenthSeconds;
    private WaitForSeconds _waitDurationSeconds;

    private CircleCollider2D _collider2D;
    private SpriteRenderer _spriteRenderer;

    private bool _isActive = false;
    private List<Enemy> _enemies = new List<Enemy>();

    private void Start()
    {
        _collider2D = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _waitTenthSeconds = new WaitForSeconds(_tickCount);
        _waitDurationSeconds = new WaitForSeconds(_durationCount);
    }

    private void OnEnable()
    {
        _inputSystem.VampirismKeyGeted += StartDrainCoroutine;
    }

    private void OnDisable()
    {
        _inputSystem.VampirismKeyGeted -= StartDrainCoroutine;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemies.Remove(enemy);
        }
    }

    private void StartDrainCoroutine()
    {
        if (_drainCoroutine != null)
        {
            StopCoroutine(_drainCoroutine);
        }

        SetActive(true);
        _drainCoroutine = StartCoroutine(DrainCoroutine());
    }

    private void StartDurationCorutine()
    {
        if (_durationCoroutine != null)
        {
            StopCoroutine(_durationCoroutine);
        }

        _durationCoroutine = StartCoroutine(DurationCorutine());
    }

    private IEnumerator DurationCorutine()
    {
        yield return _waitDurationSeconds;

        SetActive(false);
    }

    private IEnumerator DrainCoroutine()
    {
        StartDurationCorutine();

        while (_isActive)
        {
            SelectTarget();
            yield return _waitTenthSeconds;
        }

        StopCoroutine(_durationCoroutine);
        StopCoroutine(_drainCoroutine);
    }

    private void SelectTarget()
    {
        if (_enemies.Count > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {                
                if (_enemies[i].HealthOwn.CurrentHealth - _tickDrainAmount <= 0)
                {
                    Enemy dyingEnemy = _enemies[i];
                    _enemies.RemoveAt(i);
                    Drain(dyingEnemy);
                    i--;
                }
                else
                {
                    Drain(_enemies[i]);
                }
            }
        }
    }

    private void Drain(Enemy enemy)
    {
        enemy.HealthOwn.GetDamage(_tickDrainAmount);
        _playerHealth.Heal(_tickDrainAmount);
    }

    private void SetActive(bool active)
    {
        _isActive = active;
        _collider2D.enabled = active;
        _spriteRenderer.enabled = active;
    }
}
