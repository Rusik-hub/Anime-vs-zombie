using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        _currentWave = _waves[_currentWaveNumber];
        _spawnPoint = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_spawnedEnemyInWave >= _currentWave.EnemyCount)
        {
            if (_currentWaveNumber >= _waves.Count - 1)
            {
                _currentWaveNumber = 0;
                _currentWave = _waves[_currentWaveNumber];
            }

            _currentWaveNumber++;
            _currentWave = _waves[_currentWaveNumber];
            _spawnedEnemyInWave = 0;
        }

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawnedEnemyInWave++;
            _timeAfterLastSpawn = 0;
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.TemplateEnemy, _spawnPoint.position,
            _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
    }
}

[System.Serializable]

public class Wave
{
    public GameObject TemplateEnemy;
    public float Delay;
    public int EnemyCount;
}
