using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private int _enemiesCountInWave;
    [SerializeField] private Wave _currentWave;

    private int _currentWaveNumber = 0;
    private Transform _spawnPoint;
    private float _timeAfterLastSpawn;
    private int _spawnedEnemyInWave = 0;

    public event UnityAction AllEnemySpawned;

    public void StartWave()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawnedEnemyInWave++;
            _timeAfterLastSpawn = 0;
        }

        if (_spawnedEnemyInWave >= _currentWave.EnemyCount)
        {
            if (_currentWaveNumber >= _waves.Count - 1)
            {
                _currentWaveNumber = 0;
                _currentWave = _waves[_currentWaveNumber];
            }

            _currentWaveNumber++;
            AllEnemySpawned?.Invoke();
            SetWave(_currentWaveNumber);
        } 
    }

    private void Start()
    {
        _currentWave = _waves[_currentWaveNumber];
        _spawnPoint = GetComponent<Transform>();
    }

    private void Update()
    {
        StartWave();
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.TemplateEnemy, _spawnPoint.position,
            _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        _spawnedEnemyInWave = 0;
    }
}

[System.Serializable]

public class Wave
{
    public GameObject TemplateEnemy;
    public float Delay;
    public int EnemyCount;
}
