using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _lightning;

    private Rigidbody2D _rigidBody;
    private int _damaga;
    private bool _isFaceRight;
    private float _speed;
    private int _groundMask;

    public bool IsFaceRight { set => _isFaceRight = value; }

    private void Awake()
    {
        _groundMask = LayerMask.NameToLayer("Ground");
        _speed = 700;
        _rigidBody = GetComponent<Rigidbody2D>();
        _damaga = 25;
    }

    private void Start()
    {
        Fly();
        _lightning.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            enemy.GetDamage(_damaga);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == _groundMask)
        {
            Destroy(gameObject);
        }
    }

    private void Fly()
    {
        if (_isFaceRight)        
            _rigidBody.AddForce(Vector2.right * _speed);
        else
            _rigidBody.AddForce(Vector2.left * _speed);
    }
}
