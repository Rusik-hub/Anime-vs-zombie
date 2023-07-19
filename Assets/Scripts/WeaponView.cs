using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _image;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Button _sellButton;

    public event UnityAction<Weapon, WeaponView> SellButtonClicked;

    public void Render(Weapon weapon)
    {
        _name.text = weapon.Name;
        _price.text = weapon.Price.ToString();
        _image.sprite = weapon.Sprite;
        _weapon = weapon;
        TryLockItem();
    }

    private void TryLockItem()
    {
        if (_weapon.IsBuyed)
            _sellButton.interactable = false;
    }

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }

    private void OnButtonClick()
    {
        SellButtonClicked?.Invoke(_weapon, this);
    }
}
