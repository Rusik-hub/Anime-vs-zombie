using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Sprite _image;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Button _button;

    public void Render(Weapon weapon)
    {
        _name.text = weapon.Name;
        _price.text = weapon.Price.ToString();
        //_image = _weapon.Image;
        _weapon = weapon;
    }
}
