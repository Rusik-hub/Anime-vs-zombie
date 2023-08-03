using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private UnityEvent _died;
    [SerializeField] private string _name;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _speed;
    [SerializeField] private Player _target;
    [SerializeField] private Transform _currentPosition;
    [SerializeField] private Slider _slider;

    private int _minDamage = 1;
    private int _health;

    public string Name => _name;
    public GameObject Model => _model;

    public event UnityAction EnemyDied
    {
        add => _died.AddListener(value);
        remove => _died.RemoveListener(value);
    }

    public void UpdateHealthBar()
    {
        _slider.value = (float)_health / (float)_maxHealth;
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
 
        UpdateHealthBar();
    }

    private void Start()
    {
        _health = _maxHealth;
        _currentPosition = GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.Target.position, _speed * Time.deltaTime);
    }

    private void Die()
    {
        _target.TakeReward(_reward);
        _died?.Invoke();
        Destroy(this.gameObject);
    }
}
