using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent _updateMoneyCount;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private List<GameObject> _weaponModels;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private int _currentWeaponNumber;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _armWithWeapon;
    [SerializeField] private Weapon _defaultWeapon;
    [SerializeField] private Transform _target;

    private int _money = 4900;
    private int _minReward = 1;
    private int _health;
    private int _firstWeaponIndex = 0;

    public event UnityAction UpdateMoneyCount
    {
        add => _updateMoneyCount.AddListener(value);
        remove => _updateMoneyCount.RemoveListener(value);
    }

    public int Money => _money;
    public Transform Target => _target;

    public void TakeReward(int reward)
    {
        if (reward <= 0)
            _money += _minReward;
        else
            _money += reward;

        _updateMoneyCount?.Invoke();
    }

    public void AddWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
        weapon.Buy();
        var newWeapon = Instantiate(weapon.Model, _armWithWeapon.position, Quaternion.identity, _armWithWeapon);
        _weaponModels.Add(newWeapon);
        newWeapon.SetActive(false);
    }

    public bool IsEnoughMoney(int price)
    {
        if (_money - price < 0)
            return false;

        SpendMoney(price);
        _updateMoneyCount?.Invoke();
        return true;
    }

    private void Start()
    {
        _health = _maxHealth;
        AddWeapon(_defaultWeapon);
        _currentWeaponNumber = 0;
        _currentWeapon = _weapons[_currentWeaponNumber];
        _animator = GetComponent<Animator>();
        UpdateWeaponInHand(_currentWeaponNumber);
        _weaponModels[_currentWeaponNumber].SetActive(true);
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _currentWeapon.Shoot(_armWithWeapon);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeWeaponToRight();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeWeaponToLeft();
            }
        }
    }

    private void SpendMoney(int money)
    {
        _money -= money;
    }

    private void ChangeWeaponToRight()
    {
        if (_currentWeaponNumber >= _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        UpdateWeaponInHand(_currentWeaponNumber);
    }

    private void ChangeWeaponToLeft()
    {
        if (_currentWeaponNumber <= _firstWeaponIndex)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        UpdateWeaponInHand(_currentWeaponNumber);
    }

    private void UpdateWeaponInHand(int currentWeaponNumber)
    {
        _currentWeapon = _weapons[_currentWeaponNumber];

        for (int i = 0; i < _weapons.Count; i++)
        {
            _weaponModels[i].SetActive(false);
        }

        _weaponModels[_currentWeaponNumber].SetActive(true);
    }
}
