using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private InputSystem _inputSystem;

    private bool _isOnGround;
    private bool _canJump;
    private bool _isFaceRight;

    private float _overlapRadius;
    private Vector2 _direction;
    private Animator _animator;
    private Rigidbody2D _rigidBody;

    public bool IsFaceRight => _isFaceRight;

    private void Awake()
    {
        _canJump = true;
        _overlapRadius = 0.5f;
        _animator = _playerSprite.GetComponent<Animator>();
        _isFaceRight = true;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputSystem.HorisontalKeyGeted += SetDirection;
        _inputSystem.JumpKeyGeted += Jump;
    }

    private void OnDisable()
    {
        _inputSystem.HorisontalKeyGeted -= SetDirection;
        _inputSystem.JumpKeyGeted -= Jump;
    }

    private void FixedUpdate()
    {
        Move();
        CheckGround();
    }

    private void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void CheckGround()
    {
        _isOnGround = Physics2D.OverlapCircle(transform.position, _overlapRadius, _ground);
        _animator.SetBool(PlayerAnimatorData.Params.IsJump, !_isOnGround);
    }

    private void Move()
    {
        _animator.SetFloat(PlayerAnimatorData.Params.IsMove, Mathf.Abs(_direction.x));
        _rigidBody.velocity = new Vector2(_direction.x * _speed, _rigidBody.velocity.y);
        Reflect();
    }

    private void Jump()
    {
        if (_isOnGround && _canJump)
        {
            _canJump = false;
            _rigidBody.AddForce(Vector2.up * _jumpPower);
            StartCoroutine(WaitJump());
        }
    }

    private IEnumerator WaitJump()
    {
        yield return null;
        _canJump = true;
    }

    private void Reflect()
    {
        if ((_isFaceRight == false && _direction.x > 0) || (_isFaceRight && _direction.x < 0))
        {
            transform.localScale *= new Vector2(-1, 1);
            _canvas.transform.localScale *= new Vector2(-1, 1);
            _isFaceRight = !_isFaceRight;
        }
    }
}
