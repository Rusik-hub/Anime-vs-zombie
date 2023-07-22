using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NextWaveButton : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Button _button;

    //private void OnEnable()
    //{
    //    _spawner.AllEnemySpawned += ShowButton;
    //    _button.onClick.AddListener(OnNextWaveButtonClick);
    //}

    //private void OnDisable()
    //{
    //    _spawner.AllEnemySpawned -= ShowButton;
    //    _button.onClick.RemoveListener(OnNextWaveButtonClick);
    //}

    //private void ShowButton()
    //{
    //    _button.gameObject.SetActive(true);
    //}

    //private void OnNextWaveButtonClick()
    //{
    //    //_spawner.StartNextWave();
    //    _button.gameObject.SetActive(false);
    //}
}
