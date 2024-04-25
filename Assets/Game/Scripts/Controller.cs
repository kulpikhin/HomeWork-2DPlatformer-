using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Controller : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private GameObject _playerSprite;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Canvas _canvas;

    private Vector2 _direction;
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private bool _isOnGround;
    private bool _CanJump;
    private bool _isFaceRight;
    private float _overlapRadius;

    public bool IsFaceRight { get => _isFaceRight; }

    private void Awake()
    {
        _CanJump = true;
        _overlapRadius = 0.5f;
        _animator = _playerSprite.GetComponent<Animator>();
        _isFaceRight = true;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Reflect();
        Jump();
        CheckGround();
    }

    private void CheckGround()
    {
        _isOnGround = Physics2D.OverlapCircle(transform.position, _overlapRadius, _ground);
        _animator.SetBool(PlayerAnimatorData.Params.IsJump, !_isOnGround);
    }

    private void Move()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _animator.SetFloat(PlayerAnimatorData.Params.IsMove, Mathf.Abs(_direction.x));
        _rigidBody.velocity = new Vector2(_direction.x * _speed, _rigidBody.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_isOnGround && _CanJump)
            {
                _rigidBody.AddForce(Vector2.up * _jumpPower);
                _CanJump = false;
                StartCoroutine(WaitJump());
            }
        }
    }

    private IEnumerator WaitJump()
    {
        yield return null;
        _CanJump = true;
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
