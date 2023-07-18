using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private UnityEvent _died;
    [SerializeField] private string _name;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _speed;
    [SerializeField] private Player _target;
    [SerializeField] private Transform _currentPosition;

    private int _minDamage = 1;

    public string Name => _name;
    public GameObject Model => _model;

    public event UnityAction EnemyDied
    {
        add => _died.AddListener(value);
        remove => _died.RemoveListener(value);
    }

    public void Init(Player player)
    {
        _target = player;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            _health -= _minDamage;
        else
            _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Start()
    {
        _currentPosition = GetComponent<Transform>();
    }

    private void Update()
    {
        _currentPosition.Translate(Vector2.left * _speed * Time.deltaTime, Space.World);
    }

    private void Die()
    {
        _target.TakeReward(_reward);
        _died?.Invoke();
        Destroy(this.gameObject);
    }
}
