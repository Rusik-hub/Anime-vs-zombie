using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _itemContainer;

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Open()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void TrySellItem(Weapon weapon, WeaponView view)
    {
        if (_player.IsEnoughMoney(weapon.Price))
        {
            _player.AddWeapon(weapon);
            view.SellButtonClicked -= OnSellButtonClicked;
        }
        else
        {
            Debug.Log("Недостаточно денег!");
        }
    }

    private void Start()
    {
        Close();

        for (int i = 0; i < _weapons.Count; i++)
        {
            AddItem(_weapons[i]);
        }
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClicked += OnSellButtonClicked;
        view.Render(weapon);
    }

    private void OnSellButtonClicked(Weapon weapon, WeaponView view)
    {
        TrySellItem(weapon, view);
    }
}
