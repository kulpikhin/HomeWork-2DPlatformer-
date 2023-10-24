using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    public Transform _playerTarget;
    private Transform _pointTarget;
    private Transform[] _points;
    private int _countPoints;

    public Transform PlayerTarget { set => _playerTarget = value; }

    private void Awake()
    {
        _playerTarget = null;
        FillPath();
        SetTarget();
    }

    private void Update()
    {
        SwitchActions();
    }

    private void SwitchActions()
    {
        if (_playerTarget == null)
        {
            Patrol();
        }
        else
        {            
            AggressiveBehavior();
        }
    }

    private void AggressiveBehavior()
    {
        Move(_playerTarget);
    }

    private void Patrol()
    {
        Move(_pointTarget);

        if (_pointTarget.position == transform.position)
        {
            SetTarget();
        }
    }

    private void Move(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    private void SetTarget()
    {
        _pointTarget = _points[Random.Range(0, _countPoints)];
    }

    private void FillPath()
    {
        _countPoints = _path.childCount;
        _points = new Transform[_countPoints];

        for (int i = 0; i < _countPoints; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }
}
