using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _lightning;

    private int _damaga;
    private int _groundMask;

    private Rigidbody2D _rigidBody;
    private bool _isFaceRight;
    private float _speed;
    private string _maskName = "Ground";

    public bool IsFaceRight { set => _isFaceRight = value; }

    private void Awake()
    {
        _groundMask = LayerMask.NameToLayer(_maskName);
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
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.HealthOwn.DealDamage(_damaga);
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
