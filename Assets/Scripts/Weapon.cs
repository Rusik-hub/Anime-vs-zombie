using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Weapons", order = 51)]

public class Weapon : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private GameObject _model;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private bool _isBuyed;

    public string Name => _name;
    public int Price => _price;
    public GameObject Model => _model;
    public Sprite Sprite => _sprite;
    public bool IsBuyed => _isBuyed;

    public void Shoot(Transform shootPoint)
    {
        Instantiate(_bullet, shootPoint.position, Quaternion.identity);
    }

    public void Buy()
    {
        _isBuyed = true;
        Debug.Log("Покупка совершена! \"ScriptableObject/Weapon\"");
    }
}
