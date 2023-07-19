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
    [SerializeField] private List<GameObject> _weaponModels;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private int _currentWeaponNumber;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _armWithWeapon;

    [SerializeField] public List<Weapon> _testContainterForWeapons;

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
        for (int i = 0; i < _testContainterForWeapons.Count; i++)
        {
            AddWeapon(_testContainterForWeapons[i]);
        }

        _health = _maxHealth;
        _currentWeaponNumber = 0;
        _currentWeapon = _weapons[_currentWeaponNumber];
        _animator = GetComponent<Animator>();
        ChangeWeapon(_currentWeaponNumber);
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
                if (_currentWeaponNumber >= _weapons.Count - 1)
                    _currentWeaponNumber = 0;
                else
                    _currentWeaponNumber++;
                
                ChangeWeapon(_currentWeaponNumber);
            }
        }
    }

    private void ChangeWeapon(int currentWeaponNumber)
    {
        _currentWeapon = _weapons[_currentWeaponNumber];

        for (int i = 0; i < _weapons.Count; i++)
        {
            _weaponModels[i].SetActive(false);
        }

        _weaponModels[_currentWeaponNumber].SetActive(true);
    }

    private void AddWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
        var newWeapon = Instantiate(weapon.Model, _armWithWeapon.position, Quaternion.identity, _armWithWeapon);
        _weaponModels.Add(newWeapon);
        newWeapon.SetActive(false);
    }
}
