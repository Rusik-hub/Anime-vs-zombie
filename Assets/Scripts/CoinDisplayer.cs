using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CoinDisplayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        UpdateCoinCount();
    }

    private void OnEnable()
    {
        _player.UpdateMoneyCount += UpdateCoinCount;
    }

    private void OnDisable()
    {
        _player.UpdateMoneyCount -= UpdateCoinCount;
    }

    private void UpdateCoinCount()
    {
        _text.text = _player.Money.ToString();
    }
}
