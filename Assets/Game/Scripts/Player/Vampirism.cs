using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private int _tickDrainAmount;
    [SerializeField] private int _cooldawnDuration;
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Slider _cooldawnBar;

    private float _durationAmount = 6f;
    private float _tickCount = 0.1f;

    private Coroutine _drainCoroutine;
    private Coroutine _durationCoroutine;
    private Coroutine _cooldawnCoroutine;

    private WaitForSeconds _waitTenthSeconds;
    private WaitForSeconds _waitDurationSeconds;

    private bool _isActive = false;
    private bool _isCooldawn = false;

    private SpriteRenderer _spriteRenderer;
    private List<Enemy> _enemies = new List<Enemy>();

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _waitTenthSeconds = new WaitForSeconds(_tickCount);
        _waitDurationSeconds = new WaitForSeconds(_durationAmount);
        _cooldawnBar.maxValue = _cooldawnDuration;
        _cooldawnBar.value = _cooldawnDuration;
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

    private void StartCooldawnCorutine()
    {
        if(_cooldawnCoroutine != null)
        {
            StopCoroutine(_cooldawnCoroutine);
        }

        _cooldawnCoroutine = StartCoroutine(CooldawnCorutine());
    }

    private void StartDrainCoroutine()
    {
        if(!_isCooldawn && _enemies.Count > 0)
        {
            if (_drainCoroutine != null)
            {
                StopCoroutine(_drainCoroutine);
            }

            SetActive(true);
            _drainCoroutine = StartCoroutine(DrainCoroutine());
        }
    }

    private void StartDurationCorutine()
    {
        if (_durationCoroutine != null)
        {
            StopCoroutine(_durationCoroutine);
        }

        _durationCoroutine = StartCoroutine(DurationCorutine());
    }

    private IEnumerator CooldawnCorutine()
    {
        float startTime = Time.time;

        _isCooldawn = true;

        while (Time.time - startTime < _cooldawnDuration)
        {
            float passedTimeCount = Time.time - startTime;
            _cooldawnBar.value = passedTimeCount;

            yield return null;
        }

        _isCooldawn = false;
    }

    private IEnumerator DurationCorutine()
    {
        yield return _waitDurationSeconds;

        SetActive(false);
    }

    private IEnumerator DrainCoroutine()
    {
        StartDurationCorutine();
        StartCooldawnCorutine();

        while (_isActive)
        {
            SelectTarget();
            yield return _waitTenthSeconds;
        }
    }

    private void SelectTarget()
    {
        if (_enemies.Count > 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                int curentEnemyHealth = _enemies[i].HealthOwn.CurrentHealth;

                Drain(_enemies[i]);

                if (curentEnemyHealth - _tickDrainAmount <= 0)                
                    i--;                                   
            }
        }
        else
        {
            StopDrain();
        }
    }

    private void Drain(Enemy enemy)
    {
        enemy.HealthOwn.DealDamage(_tickDrainAmount);
        _playerHealth.Heal(_tickDrainAmount);
    }

    private void SetActive(bool active)
    {
        _isActive = active;
        _spriteRenderer.enabled = active;
    }

    private void StopDrain()
    {
        StopCoroutine(_drainCoroutine);
        StopCoroutine(_durationCoroutine);
        SetActive(false);
    }
}
