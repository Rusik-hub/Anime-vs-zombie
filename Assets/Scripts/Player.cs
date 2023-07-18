using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent _updateMoneyCount;
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private int _currentWeaponNumber;
    [SerializeField] private GameObject _currentWeaponModel;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _armWithWeapon;

    public event UnityAction UpdateMoneyCount
    {
        add => _updateMoneyCount.AddListener(value);
        remove => _updateMoneyCount.RemoveListener(value);
    }

    private int _money;
    private int _minReward = 1;

    public int Money => _money;

    public void TakeReward(int reward)
    {
        if (reward <= 0)
            _money += _minReward;
        else
            _money += reward;

        _updateMoneyCount?.Invoke();
    }

    private void Start()
    {
        _health = _maxHealth;
        _currentWeaponNumber = 0;
        _currentWeapon = _weapons[_currentWeaponNumber];
        _animator = GetComponent<Animator>();
        ChangeWeapon(_currentWeapon);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_armWithWeapon);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_currentWeaponNumber >= _weapons.Count - 1)
                _currentWeaponNumber = 0;
            else
                _currentWeaponNumber++;

            if (_currentWeapon != null)
            {
                Destroy(_currentWeapon);
            }

            _currentWeapon = _weapons[_currentWeaponNumber];
            ChangeWeapon(_currentWeapon);
        }
    }

    private void ChangeWeapon(Weapon weapon)
    {
        if (_currentWeaponModel != null)
        {
            Destroy(_currentWeaponModel);
        }

        Instantiate(weapon.Model, _armWithWeapon.position, Quaternion.identity);
    }
}
